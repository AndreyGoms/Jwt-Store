using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code {get;} = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpireAt {get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt {get; private set; } = null;

    public bool IsActive => VerifiedAt != null && ExpireAt == null;

    public void Verify(string code){
        if(IsActive){
            throw new Exception("Este item ja foi ativado");        
        }

        if(ExpireAt < DateTime.UtcNow){
            throw new Exception("Este codigo ja expirou");
        }

        if(!String.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase)){
            throw new Exception("Codigo de verificação inválido!");
        }

        ExpireAt = null;
        VerifiedAt= DateTime.UtcNow;
    }
}