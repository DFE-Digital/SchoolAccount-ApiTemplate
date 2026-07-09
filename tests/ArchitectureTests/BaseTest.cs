using System.Reflection;
using Domain;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Readme).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Web.Api.DependencyInjection).Assembly;
}
