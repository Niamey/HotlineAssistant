using AutoFixture;
using HotLineAssistant.Testing.Fixtures.SpecimenBuilders;
using System.Collections.Generic;
using System.Linq;

namespace HotLineAssistant.Testing.Extensions
{
    public static class AutoFixtureExtensions
	{
        public static T Construct<T>(this IFixture fixture, object parameters)
        {
            return fixture.Construct(new ConstructorSpecimenBuilder<T>(parameters));
        }

        public static IEnumerable<T> ConstructMany<T>(this IFixture fixture, int count, object parameters)
        {
            return Enumerable.Range(0, count)
                .Select(n => fixture.Construct(new ConstructorSpecimenBuilder<T>(parameters)));
        }

        private static T Construct<T>(this IFixture fixture, ConstructorSpecimenBuilder<T> builder)
        {
            fixture.Customizations.Add(builder);

            var entity = fixture.Create<T>();

            fixture.Customizations.Remove(builder);

            return entity;
        }

       /* public static TEntity Construct<TEntity>(this IFixture fixture, int id) where TEntity : IAggregateRoot
        {
            return fixture.Construct(new EntityConstructorSpecimenBuilder<TEntity>(id));
        }

        public static TEntity Construct<TEntity, TId>(this IFixture fixture, TId id)
            where TEntity : IAggregateRoot<TId>
            where TId : EntityId
        {
            return fixture.Construct(new EntityConstructorSpecimenBuilder<TEntity, TId>(id));
        }

        public static TEntity Construct<TEntity>(this IFixture fixture, int id, object parameters) where TEntity : IAggregateRoot
        {
            return fixture.Construct(new EntityConstructorSpecimenBuilder<TEntity>(id, parameters));
        }

        public static TEntity Construct<TEntity, TId>(this IFixture fixture, TId id, object parameters)
            where TEntity : IAggregateRoot<TId>
            where TId : EntityId
        {
            return fixture.Construct(new EntityConstructorSpecimenBuilder<TEntity, TId>(id, parameters));
        }

        public static IFixture CustomizeConstructor<T>(this IFixture fixture, object parameters)
        {
            fixture.Customize<T>(x => new ConstructorSpecimenBuilder<T>(parameters));

            return fixture;
        }

        public static IFixture CustomizeConstructor<TEntity>(this IFixture fixture, int id) where TEntity : IAggregateRoot
        {
            fixture.Customize<TEntity>(x => new EntityConstructorSpecimenBuilder<TEntity>(id));

            return fixture;
        }

        public static IFixture CustomizeConstructor<TEntity, TId>(this IFixture fixture, TId id)
            where TEntity : IAggregateRoot<TId>
            where TId : EntityId
        {
            fixture.Customize<TEntity>(x => new EntityConstructorSpecimenBuilder<TEntity, TId>(id));

            return fixture;
        }

        public static IFixture CustomizeConstructor<TEntity>(this IFixture fixture, int id, object parameters) where TEntity : IAggregateRoot
        {
            fixture.Customize<TEntity>(x => new EntityConstructorSpecimenBuilder<TEntity>(id, parameters));

            return fixture;
        }

        public static IFixture CustomizeConstructor<TEntity, TId>(this IFixture fixture, TId id, object parameters)
            where TEntity : IAggregateRoot<TId>
            where TId : EntityId
        {
            fixture.Customize<TEntity>(x => new EntityConstructorSpecimenBuilder<TEntity, TId>(id, parameters));

            return fixture;
        }*/
    }
}