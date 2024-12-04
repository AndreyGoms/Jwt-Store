using System.Text.RegularExpressions;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    //Regex para validar email
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public Email(string address)
    {

    if(string.IsNullOrEmpty(address))
        throw new Exception("E-mail invalido!");

    Address = address.Trim().ToLower();

    if(Address.Length < 5)
            throw new Exception("E-mail invalido!");

    if(!EmailRegex().IsMatch(Address))
            throw new Exception("E-mail invalido!");

    } 

    public string Address { get; }
    public string Hash => Address.ToBase64();

    //Se tentar passar um email ele o transforma em string
    public static implicit operator string(Email email) 
        => email.ToString();

    //se tentar passar uma string ele o transforma em email
    public static implicit operator Email(string address) 
        => new(address);
    
    public override string ToString() 
        => Address;


    [GeneratedRegex(Pattern)]
     private static partial Regex EmailRegex();
}