using Reservations.API.Models;

namespace Reservations.API.GraphQL.Types.ObjectTypes;

public class ProviderType : ObjectType<ProviderModel>
{
    protected override void Configure(IObjectTypeDescriptor<ProviderModel> descriptor)
    {
        descriptor
            .Name("Provider")
            .Key("id")
            .ResolveReferenceWith(t => ResolveUserById(default!, default!));
    }

    private static Task<User> ResolveUserById(
        string id,
        UserRepository userRepository)
    {
        return userRepository.GetUserByIdAsync(id);
    }
}