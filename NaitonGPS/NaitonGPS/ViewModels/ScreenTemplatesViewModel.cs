using NaitonGPS.Views.PickList;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
    public class ScreenTemplatesViewModel
    {
        public List<ShellContent> Screens { get; set; }

        public ScreenTemplatesViewModel()
        {
            Screens = new List<ShellContent>
            {
                new ShellContent
                {
                    Title = "WMS_Picklist", Icon = "picklist.png", Route="PickList", ContentTemplate = new DataTemplate(typeof(PickListPage))
                }//,                                
                //new Screens
                //{
                //    screenNumber = 2, ScreenTitle = "WMS_Validation", ScreenImage = "validation.png", ScreenLink = new ControlTemplate(typeof(ManagerSecondPage))
                //},
                //new Screens
                //{
                //    screenNumber = 3, ScreenTitle = "WMS_Dispatch", ScreenImage = "dispatch.png", ScreenLink = new ControlTemplate(typeof(ManagerThirdPage))
                //},
                //new Screens
                //{
                //    screenNumber = 4, ScreenTitle = "WMS_InventoryCount", ScreenImage = "crossdock.png", ScreenLink = new ControlTemplate(typeof(ManagerFourthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 5, ScreenTitle = "WMS_Droplist", ScreenImage = "moreoptions.png", ScreenLink = new ControlTemplate(typeof(ManagerFifthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 6, ScreenTitle = "WMS_Dashboard", ScreenImage = "chat.png", ScreenLink = new ControlTemplate(typeof(SecondRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 7, ScreenTitle = "WMS_MoveOrder", ScreenImage = "chat.png", ScreenLink = new ControlTemplate(typeof(DriverSecondPage))
                //},
                //new Screens
                //{
                //    screenNumber = 8, ScreenTitle = "Production_Production", ScreenImage = "chat.png", ScreenLink = new ControlTemplate(typeof(DriverThirdPage))
                //},
                //new Screens
                //{
                //    screenNumber = 9, ScreenTitle = "Production_Planner", ScreenImage = "chat.png", ScreenLink = new ControlTemplate(typeof(DriverFourthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 10, ScreenTitle = "Production_CollectMaterials", ScreenImage = "chat.png", ScreenLink = new ControlTemplate(typeof(DriverFifthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 11, ScreenTitle = "Production_Dashboard", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(ThirdRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 12, ScreenTitle = "Sales_Clients", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExSecondPage))
                //},
                //new Screens
                //{
                //    screenNumber = 13, ScreenTitle = "Sales_Companies", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExThirdPage))
                //},
                //new Screens
                //{
                //    screenNumber = 14, ScreenTitle = "Sales_Invoices", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExFourthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 15, ScreenTitle = "Sales_Orders", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExFifthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 16, ScreenTitle = "Sales_Dashboard", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 17, ScreenTitle = "Planner_NewTrackers", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplate2))
                //},
                //new Screens
                //{
                //    screenNumber = 18, ScreenTitle = "Planner_Timesheet", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateThree))
                //},
                //new Screens
                //{
                //    screenNumber = 19, ScreenTitle = "Planner_Locations", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateFour))
                //},
                //new Screens
                //{
                //    screenNumber = 20, ScreenTitle = "Planner_Tacho", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateFive))
                //},
                //new Screens
                //{
                //    screenNumber = 21, ScreenTitle = "Planner_Alerts", ScreenImage = "settings.png", ScreenLink = new ControlTemplate(typeof(FifthRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 22, ScreenTitle = "Planner_Groups", ScreenImage = "notification.png", ScreenLink = new ControlTemplate(typeof(DefaultTemplate))
                //},                
                //new Screens
                //{
                //    screenNumber = 23, ScreenTitle = "Planner_Fuel", ScreenImage = "notification.png", ScreenLink = new ControlTemplate(typeof(DefaultTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 24, ScreenTitle = "Planner_Chat", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExSecondPage))
                //},
                //new Screens
                //{
                //    screenNumber = 25, ScreenTitle = "Planner_Tasks", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExThirdPage))
                //},
                //new Screens
                //{
                //    screenNumber = 26, ScreenTitle = "Planner_Vehicles", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExFourthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 27, ScreenTitle = "Planner_Dashboard", ScreenImage = "delivery.png", ScreenLink = new ControlTemplate(typeof(SalesExFifthPage))
                //},
                //new Screens
                //{
                //    screenNumber = 28, ScreenTitle = "Driver_Navigation", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 29, ScreenTitle = "Driver_Packaging", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplate2))
                //},
                //new Screens
                //{
                //    screenNumber = 30, ScreenTitle = "Driver_Assets", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateThree))
                //},
                //new Screens
                //{
                //    screenNumber = 31, ScreenTitle = "Driver_Timesheet", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateFour))
                //},
                //new Screens
                //{
                //    screenNumber = 32, ScreenTitle = "Driver_Alerts", ScreenImage = "compass.png", ScreenLink = new ControlTemplate(typeof(FourthRoleTemplateFive))
                //},
                //new Screens
                //{
                //    screenNumber = 33, ScreenTitle = "Driver_Chat", ScreenImage = "settings.png", ScreenLink = new ControlTemplate(typeof(FifthRoleTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 34, ScreenTitle = "Driver_Tasks", ScreenImage = "notification.png", ScreenLink = new ControlTemplate(typeof(DefaultTemplate))
                //},
                //new Screens
                //{
                //    screenNumber = 35, ScreenTitle = "Driver_Dashboard", ScreenImage = "notification.png", ScreenLink = new ControlTemplate(typeof(DefaultTemplate))
                //}
            };
        }
    }
}
