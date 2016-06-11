using Windows.Security.ExchangeActiveSyncProvisioning;

namespace CNodeUwp.Common
{
    public static class EasClientDeviceInformationExtensions
    {
        /// <summary>
        /// http://edi.wang/post/2015/12/30/windows-10-uwp-report-error-page
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public static string ToDescription(this EasClientDeviceInformation deviceInfo)
        {
            return $"%0D%0A%0D%0A 设备名：{deviceInfo.FriendlyName}" +
                    $"%0D%0A 操作系统：{deviceInfo.OperatingSystem}" +
                    $"%0D%0A SKU：{deviceInfo.SystemSku}" +
                    $"%0D%0A 产品名称：{deviceInfo.SystemProductName}" +
                    $"%0D%0A 制造商：{deviceInfo.SystemManufacturer}" +
                    $"%0D%0A 固件版本：{deviceInfo.SystemFirmwareVersion}" +
                    $"%0D%0A 硬件版本：{deviceInfo.SystemHardwareVersion}";
        }
    }
}
