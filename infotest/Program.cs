// See https://aka.ms/new-console-template for more information

using System.Management;

string path = "Win32_LogicalDisk";
ManagementClass managementClass = new ManagementClass(path);
ManagementObjectCollection mn = managementClass.GetInstances();
PropertyDataCollection properties = managementClass.Properties;
Console.WriteLine(path);
foreach (PropertyData property in properties)
{
    Console.Write(property.Name + " ： ");
    foreach (ManagementObject m in mn)
    {
        Console.WriteLine(m.Properties[property.Name].Value);
    }
}
