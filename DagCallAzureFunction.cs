using AzureUtilities;
using System.Threading.Tasks;

class DagCallAzureFunction
{
    public static void Main(Args _args)
    {
        try
        {
            str tenant = "XXXXXXX";
            str serviceResourceId = "X-X-X-X-X";
            str clientId = "X-X-X-X-X";
            str appKey = "XXXXXXX";
            str functionURL = "https://XXXXXXX.azurewebsites.net/api/HttpTrigger1?code=XXXXXXX==";
            functionURL += "&name=Dag";

            // Must use 'var' instead of 'System.Threading.Tasks.Task<System.String>' because of the F&O compiler
            var t = PLUtilities.AzureFunctionHelper::authAndCallFunction(tenant, serviceResourceId, clientId, appKey, functionURL);

            t.Wait();
            str s = t.Result;
            info(s);
        }
        catch (Exception::CLRError)
        {
            System.Exception ex = ClrInterop::getLastException();
            if (ex != null)
            {
                ex = ex.get_InnerException();
                if (ex != null)
                {
                    error(ex.ToString());
                }
            }
        }
    }
}
