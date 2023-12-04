using Azure.Identity;
using Serilog;
using System.Security.Cryptography.X509Certificates;

namespace Organisation.Presentation.API.Configuration.AzureKeyVault;

public static class AzureKeyVaultConfiguration
{
    public static WebApplicationBuilder ConfigureAzureKeyVault(this WebApplicationBuilder builder)
    {

        var storeName = builder.Configuration["AzureKeyVault:KeyVaultCertStoreName"] == "Personal" ? "My" : builder.Configuration["AzureKeyVault:KeyVaultCertStoreName"];
        using var store = builder.Configuration["AzureKeyVault:KeyVaultCertStoreLocation"] == "LocalMachine" ?
                          new X509Store(storeName, StoreLocation.LocalMachine) : new X509Store(StoreLocation.CurrentUser);

        store.Open(OpenFlags.ReadOnly);
        var certs = store.Certificates.Find(X509FindType.FindByThumbprint, builder.Configuration["AzureKeyVault:KeyVaultCertThumbPrint"], false);

        if (certs.OfType<X509Certificate2>().Count() == 0)
            Log.Error("KeyVault certificate for the specified thumbprint not found.");
        else if (certs.OfType<X509Certificate2>().Count() > 1)
            Log.Error("Multiple certificates found.");

        builder.Configuration.AddAzureKeyVault(
                 new Uri(builder.Configuration["AzureKeyVault:KeyVaultBaseUrl"]),
                 new ClientCertificateCredential(
                     builder.Configuration["AzureKeyVault:AzureAppRegDirectoryId"],
                     builder.Configuration["AzureKeyVault:AzureAppRegApplicationId"],
                     certs.OfType<X509Certificate2>().Single()),
                 new PrefixKeyVaultSecretManager(builder.Configuration["AzureKeyVault:KeyVaultSettingsPrefix"])
        );

        store.Close();
        return builder;
    }
}
