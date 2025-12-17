namespace GLS.Platform.u202311077.Shared.Domain.Model.ValueObjects;

using System.Text.RegularExpressions;

public record MacAddress(string Address)
{
    
    public MacAddress() : this(string.Empty) { }
    
    
    
    public void Validate()
    {
        {
            if (!string.IsNullOrEmpty(Address))
            {
                Validate();
            }
        }
        
        if (string.IsNullOrWhiteSpace(Address))
        {
            throw new ArgumentException("MAC Address cannot be empty");
        }
            
        var regex = new Regex("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");
        
        if (!regex.IsMatch(Address))
        {
            throw new ArgumentException("Invalid MAC Address format");
        }
        
    }
    
    
    public override string ToString() => Address;
    
    
}