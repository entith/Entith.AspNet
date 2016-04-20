This is a collection of related libraries I wrote to assist with ASP.NET application development. These should probably be broken out into separate projects at some point, but I am uploading them as they are for now.

Nuget packages are available from my MyGet feed: https://www.myget.org/F/entith/api/v2

## Entith.AspNet.Domain

This library is for handling data, domain/business logic, persistence, etc. It defines interfaces for repositories, unit of work, and services. There is also a basic service implementation. Requires an ORM-specific implementation of the IRepository and IUnitOfWork interfaces.

## Entith.AspNet.Domain.EntityFramework

An Entity Framework implementation of IRepository and IUnitOfWork.

## Entith.AspNet.Domain.Identity

An implementation of Microsoft's Identity Framework that uses Entith.AspNet.Domain for persistence.

## Entith.AspNet.DependencyInjection

Provides the ISimpleRegistrationBuilder interface which is used to allow all the other libraries to bootstrap themselves and provide other DI shortcuts in a DI framwork agnostic way.

## Entith.AspNet.ModuledController

An extension of ASP.NET's MVC controller base class that allows for any number of modules to be injected (via DI). These modules can be used to add custom code to controllers in a well defined, organized manner. For example, Entith.AspNet.ModuledController.DomainTimestamp ensures that any entities that implement Entith.AspNet.Domain's ITimestampable interfaces have their timestamps set appropriately. Entith.AspNet.ModuledController.Messages provides a framework for passing messages to views, even across redirects.
