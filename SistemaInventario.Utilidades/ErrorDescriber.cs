using Microsoft.AspNetCore.Identity;

namespace SistemaInventario.Utilidades
{
    public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = "La constraseña debe tener al menos una letra minuscula"
            };
        }
    }
}
