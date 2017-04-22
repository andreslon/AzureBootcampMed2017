#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net.Mail;
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<Person> outTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string name = data?.name;

    if (name == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name in the request body");
    }

    outTable.Add(new Person()
    {
        PartitionKey = "Functions",
        RowKey = Guid.NewGuid().ToString(),
        Name = name
    });

    var result = SendMail("andreslon1992@gmail.com", "Azure", "Nuevo miembro registrado: " + name);

    return req.CreateResponse(HttpStatusCode.Created);
}

public static bool SendMail(string to, string subject, string body)
{
    try
    {
        MailMessage mailMsg = new MailMessage();
        mailMsg.To.Add(new MailAddress(to));
        mailMsg.From = new MailAddress("andreslon@outlook.com", "Azure Bootcamp");
        mailMsg.Subject = subject;
        mailMsg.Body = body;
        SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("USER", "PASS");
        smtpClient.Credentials = credentials;
        smtpClient.Send(mailMsg);
        return true;
    }
    catch (Exception ex)
    {
        return false;
    }
}
public class Person : TableEntity
{
    public string Name { get; set; }
}