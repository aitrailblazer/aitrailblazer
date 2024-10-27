 
 using AITGraph.Sdk.Models;

 public class ContactViewModel
{
    public string Id { get; set; }
    public Contact OriginalContact { get; set; }
    public string DisplayName { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string EmailAddress { get; set; }
    public string EmailAddressesFormatted { get; set; }
    public string MobilePhone { get; set; }
    public string BusinessPhonesFormatted { get; set; }
    public string HomePhonesFormatted { get; set; }
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public string OfficeLocation { get; set; }
    public string ImAddressesFormatted { get; set; }
    public string BirthdayFormatted { get; set; }
    public string NickName { get; set; }
    public string MiddleName { get; set; }
    public string PersonalNotes { get; set; }
    public string SpouseName { get; set; }
    public string Department { get; set; }
    public string Manager { get; set; }
    public string AssistantName { get; set; }
    public string YomiGivenName { get; set; }
    public string YomiSurname { get; set; }
    public string YomiCompanyName { get; set; }
    public PhysicalAddress BusinessAddress { get; set; }
    public string BusinessAddressFormatted { get; set; }
    public string HomeAddressFormatted { get; set; }
    public string OtherAddressFormatted { get; set; }
    public string Profession { get; set; }
    public string Title { get; set; }
    public string ChildrenFormatted { get; set; }
    public string Gender { get; set; }

    public ContactViewModel(Contact contact = null)
    {
        OriginalContact = contact ?? new Contact();
        Id = contact?.Id ?? "";
        DisplayName = contact?.DisplayName ?? "New Contact";
        GivenName = contact?.GivenName ?? "";
        Surname = contact?.Surname ?? "";
        EmailAddressesFormatted = contact?.EmailAddresses != null && contact.EmailAddresses.Any()
            ? string.Join(", ", contact.EmailAddresses.Select(e => e.Address))
            : "";
        EmailAddress = contact?.EmailAddresses != null && contact.EmailAddresses.Any()
                    ? contact.EmailAddresses.First().Address
                    : "";
        if (contact?.Phones != null && contact.Phones.Any())
        {
            MobilePhone = contact.Phones
                .FirstOrDefault(phone => phone.Type == PhoneType.Mobile)?.Number ?? "";
            BusinessPhonesFormatted = string.Join(", ", contact.Phones
                .Where(phone => phone.Type == PhoneType.Business)
                .Select(phone => phone.Number));
            HomePhonesFormatted = string.Join(", ", contact.Phones
                .Where(phone => phone.Type == PhoneType.Home)
                .Select(phone => phone.Number));
        }
        else
        {
            MobilePhone = "";
            BusinessPhonesFormatted = "";
            HomePhonesFormatted = "";
        }

        CompanyName = contact?.CompanyName ?? "";
        JobTitle = contact?.JobTitle ?? "";
        OfficeLocation = contact?.OfficeLocation ?? "";
        ImAddressesFormatted = contact?.ImAddresses != null && contact.ImAddresses.Any()
            ? string.Join(", ", contact.ImAddresses)
            : "";
        BirthdayFormatted = contact?.Birthday.HasValue == true
            ? contact.Birthday.Value.ToLocalTime().ToString("d")
            : "";
        NickName = contact?.NickName ?? "";
        MiddleName = contact?.MiddleName ?? "";
        PersonalNotes = contact?.PersonalNotes ?? "";
        SpouseName = contact?.SpouseName ?? "";
        Department = contact?.Department ?? "";
        Manager = contact?.Manager ?? "";
        AssistantName = contact?.AssistantName ?? "";
        YomiGivenName = contact?.YomiGivenName ?? "";
        YomiSurname = contact?.YomiSurname ?? "";
        YomiCompanyName = contact?.YomiCompanyName ?? "";

        if (contact?.PostalAddresses != null && contact.PostalAddresses.Any())
        {
            BusinessAddress = contact.PostalAddresses
                .FirstOrDefault(addr => addr.Type == PhysicalAddressType.Business);
            BusinessAddressFormatted = GetAddressFormatted(BusinessAddress);
            HomeAddressFormatted = GetAddressFormatted(contact.PostalAddresses
                .FirstOrDefault(addr => addr.Type == PhysicalAddressType.Home));
            OtherAddressFormatted = GetAddressFormatted(contact.PostalAddresses
                .FirstOrDefault(addr => addr.Type == PhysicalAddressType.Other));
        }
        else
        {
            BusinessAddress = new PhysicalAddress();
            BusinessAddressFormatted = "";
            HomeAddressFormatted = "";
            OtherAddressFormatted = "";
        }

        Profession = contact?.Profession ?? "";
        Title = contact?.Title ?? "";
        ChildrenFormatted = contact?.Children != null && contact.Children.Any()
            ? string.Join(", ", contact.Children)
            : "";
        Gender = contact?.Gender ?? "";
    }
    private string GetAddressFormatted(PhysicalAddress address)
    {
        if (address == null) return "N/A";

        var components = new List<string>
        {
            address.Street, address.City, address.State, address.PostalCode, address.CountryOrRegion
        }.Where(c => !string.IsNullOrWhiteSpace(c));

        return components.Any() ? string.Join(", ", components) : "N/A";
    }
}
