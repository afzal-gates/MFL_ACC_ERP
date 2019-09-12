using ERP.Shared;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Extenssion
{
    public static class ReportExtenssionMethod
    {

        public static FileContentResult ToFile(ReportType reportType, string path, List<ReportDataSource> reportDataSources, DeviceInformation deviceInfos)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = path;
            reportDataSources.ForEach(x => localReport.DataSources.Add(x));
            string mimeType;
            string encoding;
            string fileNameExtension;

            DeviceInformation model = deviceInfos ?? new DeviceInformation();

            StringBuilder deviceInfoSb = new StringBuilder();

            deviceInfoSb.Append("<DeviceInfo>");
            deviceInfoSb.AppendFormat(String.Format("<OutputFormat>{0}</OutputFormat>", model.OutputFormat));
            deviceInfoSb.AppendFormat(String.Format("<PageWidth>{0}in</PageWidth>", model.PageWidth));
            deviceInfoSb.AppendFormat(String.Format("<PageHeight>{0}in</PageHeight>", model.PageHeight));
            deviceInfoSb.AppendFormat(String.Format("<MarginTop>{0}in</MarginTop>", model.MarginTop));
            deviceInfoSb.AppendFormat(String.Format("<MarginLeft>{0}in</MarginLeft>", model.MarginLeft));
            deviceInfoSb.AppendFormat(String.Format("<MarginRight>{0}in</MarginRight>", model.MarginRight));
            deviceInfoSb.AppendFormat(String.Format("<MarginBottom>{0}in</MarginBottom>", model.MarginBottom));
            deviceInfoSb.Append("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType.ToString("g"), deviceInfoSb.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return new FileContentResult(renderedBytes, mimeType);
        }

        public static FileContentResult ToFile(ReportType reportType, string path, List<ReportDataSource> reportDataSources, DeviceInformation deviceInfos, List<ReportParameter> parameters)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = path;
            reportDataSources.ForEach(x => localReport.DataSources.Add(x));
            localReport.SetParameters(parameters);
            string mimeType;
            string encoding;
            string fileNameExtension;

            DeviceInformation model = deviceInfos ?? new DeviceInformation();

            StringBuilder deviceInfoSb = new StringBuilder();

            deviceInfoSb.Append("<DeviceInfo>");
            deviceInfoSb.AppendFormat(String.Format("<OutputFormat>{0}</OutputFormat>", model.OutputFormat));
            deviceInfoSb.AppendFormat(String.Format("<PageWidth>{0}in</PageWidth>", model.PageWidth));
            deviceInfoSb.AppendFormat(String.Format("<PageHeight>{0}in</PageHeight>", model.PageHeight));
            deviceInfoSb.AppendFormat(String.Format("<MarginTop>{0}in</MarginTop>", model.MarginTop));
            deviceInfoSb.AppendFormat(String.Format("<MarginLeft>{0}in</MarginLeft>", model.MarginLeft));
            deviceInfoSb.AppendFormat(String.Format("<MarginRight>{0}in</MarginRight>", model.MarginRight));
            deviceInfoSb.AppendFormat(String.Format("<MarginBottom>{0}in</MarginBottom>", model.MarginBottom));
            deviceInfoSb.Append("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType.ToString("g"), deviceInfoSb.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return new FileContentResult(renderedBytes, mimeType);
        }

        public static FileContentResult ToFile(ReportType reportType, string path, List<ReportDataSource> reportDataSources)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = path;
            reportDataSources.ForEach(x => localReport.DataSources.Add(x));

            string mimeType;
            string encoding;
            string fileNameExtension;

            DeviceInformation model = new DeviceInformation();

            StringBuilder deviceInfoSb = new StringBuilder();

            deviceInfoSb.Append("<DeviceInfo>");
            deviceInfoSb.AppendFormat(String.Format("<OutputFormat>{0}</OutputFormat>", model.OutputFormat));
            deviceInfoSb.AppendFormat(String.Format("<PageWidth>{0}in</PageWidth>", model.PageWidth));
            deviceInfoSb.AppendFormat(String.Format("<PageHeight>{0}in</PageHeight>", model.PageHeight));
            deviceInfoSb.AppendFormat(String.Format("<MarginTop>{0}in</MarginTop>", model.MarginTop));
            deviceInfoSb.AppendFormat(String.Format("<MarginLeft>{0}in</MarginLeft>", model.MarginLeft));
            deviceInfoSb.AppendFormat(String.Format("<MarginRight>{0}in</MarginRight>", model.MarginRight));
            deviceInfoSb.AppendFormat(String.Format("<MarginBottom>{0}in</MarginBottom>", model.MarginBottom));
            deviceInfoSb.Append("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType.ToString("g"), deviceInfoSb.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return new FileContentResult(renderedBytes, mimeType);
        }

        public static FileContentResult ToFile(ReportType reportType, string path, List<ReportDataSource> reportDataSources, List<ReportParameter> parameters)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = path;
            reportDataSources.ForEach(x => localReport.DataSources.Add(x));
            localReport.SetParameters(parameters);
            string mimeType;
            string encoding;
            string fileNameExtension;

            DeviceInformation model = new DeviceInformation();

            StringBuilder deviceInfoSb = new StringBuilder();

            deviceInfoSb.Append("<DeviceInfo>");
            deviceInfoSb.AppendFormat(String.Format("<OutputFormat>{0}</OutputFormat>", model.OutputFormat));
            deviceInfoSb.AppendFormat(String.Format("<PageWidth>{0}in</PageWidth>", model.PageWidth));
            deviceInfoSb.AppendFormat(String.Format("<PageHeight>{0}in</PageHeight>", model.PageHeight));
            deviceInfoSb.AppendFormat(String.Format("<MarginTop>{0}in</MarginTop>", model.MarginTop));
            deviceInfoSb.AppendFormat(String.Format("<MarginLeft>{0}in</MarginLeft>", model.MarginLeft));
            deviceInfoSb.AppendFormat(String.Format("<MarginRight>{0}in</MarginRight>", model.MarginRight));
            deviceInfoSb.AppendFormat(String.Format("<MarginBottom>{0}in</MarginBottom>", model.MarginBottom));
            deviceInfoSb.Append("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType.ToString("g"), deviceInfoSb.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return new FileContentResult(renderedBytes, mimeType);
        }

        public static FileContentResult ToFile(ReportType reportType, string path, List<ReportDataSource> reportDataSources, DeviceInformation deviceInfos, ref byte[] renderedBytesRef)
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = path;
            reportDataSources.ForEach(x => localReport.DataSources.Add(x));
            string mimeType;
            string encoding;
            string fileNameExtension;

            DeviceInformation model = deviceInfos ?? new DeviceInformation();

            StringBuilder deviceInfoSb = new StringBuilder();

            deviceInfoSb.Append("<DeviceInfo>");
            deviceInfoSb.AppendFormat(String.Format("<OutputFormat>{0}</OutputFormat>", model.OutputFormat));
            deviceInfoSb.AppendFormat(String.Format("<PageWidth>{0}in</PageWidth>", model.PageWidth));
            deviceInfoSb.AppendFormat(String.Format("<PageHeight>{0}in</PageHeight>", model.PageHeight));
            deviceInfoSb.AppendFormat(String.Format("<MarginTop>{0}in</MarginTop>", model.MarginTop));
            deviceInfoSb.AppendFormat(String.Format("<MarginLeft>{0}in</MarginLeft>", model.MarginLeft));
            deviceInfoSb.AppendFormat(String.Format("<MarginRight>{0}in</MarginRight>", model.MarginRight));
            deviceInfoSb.AppendFormat(String.Format("<MarginBottom>{0}in</MarginBottom>", model.MarginBottom));
            deviceInfoSb.Append("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType.ToString("g"), deviceInfoSb.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            renderedBytesRef = renderedBytes;

            return new FileContentResult(renderedBytes, mimeType);
        }

    }
}