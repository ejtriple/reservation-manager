using Reservations.API.GraphQL;
using Reservations.API.Types;

namespace Reservations.API;

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        // descriptor
        //     .Name("Query")
        //     .Field("me")
        //     .Type<NonNullType<UserType>>()
        //     .Resolve(ctx => ctx.Service<UserRepository>().GetUserByIdAsync("1"));

        //Appointments
        descriptor
            .Field(f => f.GetProviders())
            .Type<NonNullType<ListType<ProviderType>>>();

        descriptor
            .Field(_ => _.GetAppointmentSlots())
            .Type<NonNullType<ListType<AppointmentSlotType>>>();
    }
}