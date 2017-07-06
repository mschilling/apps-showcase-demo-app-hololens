using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {

    public static string getAppsUrl = "http://singleuseapps.com:8080/apps/api/v1/customers/12/projects/92/applications";
    public static string deviceApiUrl = "http://192.168.13.148/upload.php?id=12&projectid=92&applicationid=2&filename=android-debug.apk";

    public static string generateAppsurl(int customerid, int projectid)
    {
        string baseUrl = "http://singleuseapps.com:8080/apps/api/v1/customers/12/projects/92/applications";
        baseUrl = baseUrl.Replace("12", customerid.ToString());
        baseUrl = baseUrl.Replace("92", projectid.ToString());

        return baseUrl;
    }

}
