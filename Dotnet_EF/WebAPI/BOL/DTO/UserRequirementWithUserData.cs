public class UserRequirementWithUserData
{
    public int RequirementID{get;set;}
    public int UserID { get; set;}
    public string NameFirst { get; set; }
    public string NameLast { get; set; }
    public string PhoneNumber { get; set; }
    public string Skills { get; set; }
    public string Wages { get; set; }
    public string Address { get; set; }
    public string Date { get; set; }
    public override string ToString()
    {
        return $"RequirementID: {RequirementID}, UserID: {UserID}, Name: {NameFirst} {NameLast}, Phone Number: {PhoneNumber}, Skills: {Skills}, Wages: {Wages}, Address: {Address}, Date: {Date}";
    }
    
}
