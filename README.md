**DO NOT USE** - These libraries are bad and should not be used. I have learned much since I made them, and I have no intention of ever using them on a new project. My projects that do use these have resulted in "viscus" code that is not easy to change or add to, hard to follow or predict logic, and more worthless boilerplate code than I could ever have imagined. The only place where these libraries may have had a place is in the most basic of CRUD applications, but even then, you are better off just implementing your own repository class.

Note to self: stop trying to make frameworks out of everything.

Coming soon: CQRS + Event Sourcing framework

----

Service and repository libraries for ORM-agnostic data access and domain logic/business rules.

# Overview

There are three main types of objects that you'll be working with:

* __Entities__ - These are your POCO database entities
* __Services__ - These provide ORM-agnostic CRUD operations to your database; this is where most custom read functions will be defined (ex. eager loading certain properties)
* __Logic Units__ - These are used to define discreet bits of business logic

Here are a few other object you'll likely use, but will likely not need to implement/derive from yourself:

* __Repository__ - These will have a specific implementation for each supported ORM and provide basic database CRUD operations; typically used by Services and Logic Units
* __Unit of Work__ - This will also have a specific implementation for each ORM and provide methods for saving, reviewing, and clearing pending changes; typically used by Services and Logic Units
* __Logic Manager__ - Handles business logic by intercepting calls to Repository methods and calling appropriate Logic Unit methods

# Getting Started

You'll need to install the `Entith.AspNet.Domain` library, an ORM implementation (currently EntityFramework6 and EntityFrameworkCore are supported), and optionally, a DI framework implementation (currently Autofac3 and Autofac4 are supported). For most examples, we'll be using EntityFrameworkCore and Autofac4.

```
PM> Install-Package Entith.AspNet.Domain
PM> Install-Package Entith.AspNet.Domain.EntityFrameworkCore
PM> Install-Package Entith.AspNet.DependencyInjection.Autofac4
```

For the most basic set up, you'll need to define your entities and wire up your ORM and dependency injection. There are some helpers to simplify the DI setup.

Lets start with a couple of entities:

```C#
// All entities should implement IEntity. The type parameter specifies the type
// of the ID field
public class Post : IEntity<int>
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public int AuthorId { get; set; }

  public virtual User Author { get; set; }
}

public class User : IEntity<int>
{
  public int Id { get; set; }
  public string Username { get; set; }

  public virtual ICollection<Post> Posts { get; set; }
}
```

You'll then need to configure your ORM with these entities and relationships. In the case of EntityFramework, you'll need to create a `DbContext` and wire up the relationship between `Post` and `User`.

Each DI implementation should provide a means of getting an `IDomainBuilder` instance. In the case of Autofac, there is an extension method `GetDomainBuilder()` on Autofac's `ContainerBuilder` class.

For now, we will use the basic Service class provided by the library and the basic Repository class provided by the EntityFramework implementation:

```C#
// builder is the Autoface ContainerBuilder used for the project.
IDomainBuilder domainBuilder = builder.GetDomainBuilder();

// Bootstrap some of the basic, global classes.
// Note that SampleDbContext should also needs to be registered with DI, but
// that is outside the scope of this document
domainBuilder.BootstrapDomain<EfUnitOfWork<SampleDbContext>>();

domainBuilder.RegisterEntity<Post, int>()
  .WithDefaultService()
  .WithRepository<EfRepository<Post, int, SampleDbContext>, IEfRepository<Post, int>>();

domainBuilder.RegisterEntity<User, int>()
  .WithDefaultService()
  .WithRepository<EfRepository<User, int, SampleDbContext>, IEfRepository<User, int>>();
```

And that should be all. You can now get instances of `IDomainService<Post, int>` and `IDomainService<User, int>` from the DI container and use them in your classes.

# Entities

All entities should implement `IEntity<TKey>`, which just provides a primary key property `Id` of type `TKey`:

```c#
public class SampleEntity : IEntity<int>
{
  public int Id { get; set; }
}
```

You can use any type for the key that your ORM supports:

```C#
public class StringEntity : IEntity<string>
{
  public string Id { get; set; }
}

public class FloatEntity : IEntity<float>
{
  public float Id { get; set; }
}

public class GuidEntity : IEntity<Guid>
{
  public Guid Id { get; set; }
}
```

## Custom Primary Key

Unfortunately there is no clean way to change the name of the primary key `Id` property. If you need to do so, you have two options: map the property to a different table column in your ORM or make the getter/setter methods for `Id` read/write from/to another property and ignore the `Id` property in your ORM.

```C#
public class Customer : IEntity<int>
{
  public int Id
  {
    get { return CustomerNumber; }
    set { CustomerNumber = value; }
  }
  public int CustomerNumber { get; set; }
}
```

# Services

If you don't need any special methods in your service, you can use the stock `DomainService<TEntity, TKey>` class. Otherwise, see instructions below on creating custom Service classes.

To register services using the DI helpers:

```C#
// Stock DomainService class
domainBuilder.RegisterEntity<Person, int>()
  .WithDefaultService()
  .WithRepository(...);

// Custom service class
domainBuilder.RegisterEntity<Post, int>()
  .WithService<PostService, IPostService>()
  .WithRepository(...);
```

Don't forget to register an appropriate `IRepository` for each entity.

## Custom methods

If you wish to include custom methods, you'll have to implement the `IDomainService` interface yourself. The easiest way to do this is to extend the `IDomainService` interface and `DomainService` class :

```C#
public interface IPersonService : IDomainService<Person, int>
{
    // Add your custom method signatures here
}

public class PersonService : DomainService<Person, int>, IPersonService
{
    public PersonService(IUnitOfWork uow, IRepositoryManager repositoryManager)
        :base (uow, repositoryManager)
    {
        // Add any additional dependencies here
    }

    // Add custom methods and override existing ones
}
```

You can then create new methods, override existing ones, and inject your business logic where appropriate. In your DI framework, register `PersonService` as an `IPersonService`. Don't forget to also register an appropriate `IRepository<Person, int>` implementation for your ORM.

## Eager loading

I have not found a good way to abstract eager loading, so unfortunately it has to be handled in an ORM specific way. Like above, create an interface for your service and add all the custom methods, including the ones that will use eager loading. Then create an abstract implementation of that interface, leaving the eager loading methods abstract and implementing the others. Lastly extend your abstract service and provide ORM specific implementations of the eager loading classes. Note that you'll want to extend `DomainService<TEntity, TKey, TRepository>` instead of `DomainService<TEntity, TKey>` to get the ORM specific `IRepository` wired in correctly.

```C#
public interface IPersonService : IDomainService<Person, int>
{
    Person CustomBasicGet(int id);
    Person CustomEagerGet(int id);
}

// This abstract class is still ORM-agnostic; your can use it as-is for any ORM.
public abstract class PersonService<TRepository>
    : DomainService<Person, int, TRepository>, IPersonService
    where TRepository : IRepository<Person, int>
{
    public PersonService(IUnitOfWork uow, IRepositoryManager repositoryManager)
        :base (uow, repositoryManager)
    {
        // Add any additional dependencies here
    }

    // The eager loading methods will be left abstract here
    public abstract Person CustomEagerGet(int id);

    // Implement the non-eager loading methods
    public Person CustomBasicGet(int id)
    {
        // Do stuff
        return Repository.Get(id);
    }

    // Since this class is abstract, code here can call the eager loading
    // methods and assume they will be correctly implemented elsewhere.
}

// Using EntityFramework repository in this example.
// If you change ORMs, you will only need to replace this class, the interface
// and abstract class should still work.
public class PersonService : PersonService<IEfRepository<Person, int>>
{
    public PersonService(IUnitOfWork uow, IRepositoryManager repositoryManager)
        :base (uow, repositoryManager)
    {
        // Add any additional dependencies here
    }

    public override Person CustomEagerGet(int id)
    {
        // Do eager loading stuff
        // Note: _repository will give you a reference to the generic
        // IRepository. CustomRepository will give you access to the ORM
        // specific repository

        // Ef6
        return CustomRepository.Get(id, p => p.Address);

        // EfCore
        return CustomRepository.Get(id).Include(p => p.Address);
    }
}

```

# Logic Units

Domain logic should be implemented by extending the `LogicUnit` class. Each logic unit should represent a discreet piece of business logic. To create a logic unit for an entity, extend the `LogicUnit` class:

```C#
public class PersonLogicUnit : LogicUnit<Person, int>
{
    protected override void Init()
    {
        // Any initialization code here.
    }
}
```

You'll need to register the `LogicUnit` in your DI framework:

```C#
domainBuilder.RegisterLogicUnit<PersonLogicUnit>();
```

To do the actual work, your can override any of the following methods that LogicUnit provide:
* `void OnAdd(TEntity entity)` - Called right before any calls to the repositories `Add()` method.
* `void OnRemove(TEntity entity)` - Called right before any calls to the repositories `Remove()` method.
* `void PostAdd(TEntity entity)` - Called right after any calls to the repositories `Add()` method.
* `void PostRemove(TEntity entity)` - Called right after any calls to the repositories `Remove()` method.
* `void OnSaveChanges()` - Called right before the `UnitOfWork.SaveChanges()` method.
* `void PostSaveChanges()` - Called right after the `UnitOfWork.SaveChanges()` method.

__Note:__ `AddRange()` and `RemoveRange()` call `Add()` and `Remove()` for each entity passed to them.

`LogicUnit` also provide the `ChangeTracker` and `RepoManager` properties that you can use in any of the above methods or the `Init()` method. `ChangeTracker` provides `GetAdded()`, `GetModified()`, and `GetRemoved()`. `RepoManager` lets you get any registered repository.
