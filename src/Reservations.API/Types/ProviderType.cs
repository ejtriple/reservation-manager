using Reservations.API.Models;

namespace Reservations.API.Types;

public class ProviderType : ObjectType<ProviderModel>
{
    protected void Configure(IObjectTypeDescriptor<ProviderModel> descriptor)
    {
        descriptor.Name("Provider");
        descriptor.Field(_ => _.Id).Name("id").Type<NonNullType<IdType>>();
        descriptor.Field(_ => _.Name).Name("name").Type<NonNullType<StringType>>();
        descriptor.Field(_ => _.UserName).Name("username").Type<NonNullType<StringType>>();
    }
}