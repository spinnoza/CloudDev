using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class NullPermissionStore : IPermissionStore
    {
        public Task<bool> IsGrantedAsync([NotNull] string name, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return Task.FromResult(true);
        }

        public Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return Task.FromResult(new MultiplePermissionGrantResult(names, PermissionGrantResult.Prohibited));
        }
    }
}
