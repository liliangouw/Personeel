﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    /// <summary>
    /// Window Api名称
    /// </summary>
    public enum WindowAPIType
    {
        /// <summary>
        /// 内存
        /// </summary>
        Win32_PhysicalMemory,
        /// <summary>
        /// CPU
        /// </summary>
        Win32_Processor,
        /// <summary>
        /// 硬盘
        /// </summary>
        Win32_DiskDrive,
        /// <summary>
        /// 电脑型号
        /// </summary>
        Win32_ComputerSystemProduct,
        /// <summary>
        /// 分辨率
        /// </summary>
        Win32_DesktopMonitor,
        /// <summary>
        /// 显卡细节
        /// </summary>
        Win32_VideoController,
        /// <summary>
        /// 操作系统
        /// </summary>
        Win32_OperatingSystem,
        /// <summary>
        /// 网络适配器
        /// </summary>
        Win32_NetworkAdapter,
        /// <summary>
        /// 网络适配器设置
        /// </summary>
        Win32_NetworkAdapterConfiguration,
        /// <summary>
        /// 操作系统登录
        /// </summary>
        Win32_ComputerSystem,
        /// <summary>
        /// 键盘
        /// </summary>
        Win32_Keyboard,
        /// <summary>
        /// 点输入设备，包括鼠标
        /// </summary>
        Win32_PointingDevice,
        /// <summary>
        /// 主板
        /// </summary>
        Win32_BaseBoard,
        /// <summary>
        /// BIOS芯片
        /// </summary>
        Win32_BIOS,
        /// <summary>
        /// 光盘驱动盘
        /// </summary>
        Win32_CDROMDrive,
        /// <summary>
        /// 并口
        /// </summary>
        Win32_ParallelPort,
        /// <summary>
        /// 串口
        /// </summary>
        Win32_SerialPort,
        /// <summary>
        /// 串口配置
        /// </summary>
        Win32_SerialPortConfiguration,
        /// <summary>
        /// 多媒体，一般指声卡
        /// </summary>
        Win32_SoundDevice,
        /// <summary>
        /// 主板插槽（ISA&PCI&AGP)
        /// </summary>
        Win32_SystemSlot,
        /// <summary>
        /// USB控制器
        /// </summary>
        Win32_USBController,
        /// <summary>
        /// 打印机
        /// </summary>
        Win32_Printer,
        /// <summary>
        /// 打印机设置
        /// </summary>
        Win32_PrinterConfiguration,
        /// <summary>
        /// 打印机任务
        /// </summary>
        Win32_PrintJob,
        /// <summary>
        /// 打印机端口
        /// </summary>
        Win32_TCPIPPrinterPort,
        /// <summary>
        /// Modem
        /// </summary>
        Win32_POTSModem,
        /// <summary>
        /// MODEM端口
        /// </summary>
        Win32_POTSModemToSerialPort,
        /// <summary>
        /// 显卡
        /// </summary>
        Win32_DisplayConfiguration,
        /// <summary>
        /// 显卡设置
        /// </summary>
        Win32_DisplayControllerConfiguration,
        /// <summary>
        /// 显示支持显示模式
        /// </summary>
        Win32_VideoSettings,
        /// <summary>
        /// 时区
        /// </summary>
        Win32_TimeZone,
        /// <summary>
        /// 驱动程序
        /// </summary>
        Win32_SystemDriver,
        /// <summary>
        /// 磁盘分区
        /// </summary>
        Win32_DiskPartition,
        /// <summary>
        /// 逻辑磁盘
        /// </summary>
        Win32_LogicalDisk,
        /// <summary>
        /// 逻辑磁盘所在分区及始末位置
        /// </summary>
        Win32_LogicalDiskToPartition,
        /// <summary>
        /// 逻辑内存配置
        /// </summary>
        Win32_LogicalMemoryConfiguration,
        /// <summary>
        /// 系统页文件信息
        /// </summary>
        Win32_PageFile,
        /// <summary>
        /// 页文件设置
        /// </summary>
        Win32_PageFileSetting,
        /// <summary>
        /// 系统启动配置
        /// </summary>
        Win32_BootConfiguration,
        /// <summary>
        /// 系统自动启动程序
        /// </summary>
        Win32_StartupCommand,
        /// <summary>
        /// 系统安装的服务
        /// </summary>
        Win32_Service,
        /// <summary>
        /// 系统管理组
        /// </summary>
        Win32_Group,
        /// <summary>
        /// 系统组账号
        /// </summary>
        Win32_GroupUser,
        /// <summary>
        /// 用户账户
        /// </summary>
        Win32_UserAccount,
        /// <summary>
        /// 系统进程
        /// </summary>
        Win32_Process,
        /// <summary>
        /// 系统线程
        /// </summary>
        Win32_Thread,
        /// <summary>
        /// 共享
        /// </summary>
        Win32_Share,
        /// <summary>
        /// 已安装的网络客户端
        /// </summary>
        Win32_NetworkClient,
        /// <summary>
        /// 已安装的网络协议
        /// </summary>
        Win32_NetworkProtocol,
    }
}
