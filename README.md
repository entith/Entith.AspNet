Service and repository libraries for ORM-agnostic data access and domain logic/business rules.

# Getting Started

TODO

# Entities

TODO

## Custom Primary Key

TODO

# Services

If you don't need any special methods or business logic, you can use the stock `DomainService<TEntity, TKey>` class. In your DI framework, register `DomainService<TEntity, TKey` as an `IDomainService<TEntity, TKey>`, obviously setting `TEntity` and `TKey` to your entity type and its key type. In the constructors of dependent classes, use `IDomainService<TEntity, TKey>`. You will also want to register the ORM-specific implementation of the `IRepository` class as `IRepository<TEntity, TKey>`.

## Custom methods & domain logic

If you wish to include custom methods or any business logic, you'll have to implement the `IDomainService` interface yourself. The easiest way to do this is to extend the `IDomainService` interface and `DomainService` class :

```C#
public interface IPersonService : IDomainService<Person, int>
{
    // Add your custom method signatures here
}

public class PersonService : DomainService<Person, int>, IPersonService
{
    public DomainService(IUnitOfWork uow)
        : base(uow)
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
    public DomainService(IUnitOfWork uow)
        : base(uow)
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

    // Add any business logic code as well.
    // Since this class is abstract, code here can call the eager loading
    // methods and assume they will be correctly implemented elsewhere.
}

// Using EntityFramework repository in this example.
// If you change ORMs, you will only need to replace this class, the interface
// and abstract class should still work.
public class PersonService : PersonService<IEfRepository<Person, int>>
{
    public DomainService(IUnitOfWork uow)
        : base(uow)
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
