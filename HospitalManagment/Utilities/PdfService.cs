using HospitalManagment.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HospitalManagment.Utilities
{
    public class PdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GeneratePatientsPdf(List<Patient> patients)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // Header
                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(inner =>
                            {
                                inner.Item()
                                     .Text("تقرير المرضى")
                                     .FontSize(18)
                                     .Bold()
                                     .FontColor(Colors.Blue.Darken4);

                                inner.Item()
                                     .Text($"تاريخ التقرير: {DateTime.Now:yyyy-MM-dd HH:mm}")
                                     .FontSize(10)
                                     .FontColor(Colors.Grey.Darken1);
                            });

                            row.ConstantItem(80)
                               .Height(40)
                               .Background(Colors.Blue.Darken4)
                               .AlignCenter()
                               .AlignMiddle()
                               .Text("Medicare")
                               .FontColor(Colors.White)
                               .Bold();
                        });

                        col.Item()
                           .PaddingTop(8)
                           .BorderBottom(1)
                           .BorderColor(Colors.Blue.Darken4)
                           .Text("");
                    });

                    // Content
                    page.Content().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("الاسم الكامل").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("رقم الهاتف").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("تاريخ الميلاد").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("الجنس").FontColor(Colors.White).Bold();
                        });

                        for (int i = 0; i < patients.Count; i++)
                        {
                            var p = patients[i];
                            var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten3;

                            table.Cell().Background(bg).Padding(5).Text((i + 1).ToString());
                            table.Cell().Background(bg).Padding(5).Text(p.FullName ?? "");
                            table.Cell().Background(bg).Padding(5).Text(p.PhoneNumber ?? "");
                            table.Cell().Background(bg).Padding(5).Text(p.DateOfBirth.ToString("yyyy-MM-dd"));
                            table.Cell().Background(bg).Padding(5).Text(p.Gender ?? "");
                        }
                    });

                    // Footer
                    page.Footer().BorderTop(1).BorderColor(Colors.Grey.Lighten2).PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem()
                           .Text($"إجمالي: {patients.Count}")
                           .FontSize(10)
                           .FontColor(Colors.Grey.Darken1);

                        row.RelativeItem()
                           .AlignRight()
                           .Text("نظام إدارة المستشفى — Medicare")
                           .FontSize(10)
                           .FontColor(Colors.Grey.Darken1);
                    });
                });
            }).GeneratePdf();
        }

        public byte[] GenerateAppointmentsPdf(List<Appointment> appointments)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // Header
                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(inner =>
                            {
                                inner.Item()
                                     .Text("تقرير المواعيد")
                                     .FontSize(18)
                                     .Bold()
                                     .FontColor(Colors.Blue.Darken4);

                                inner.Item()
                                     .Text($"تاريخ التقرير: {DateTime.Now:yyyy-MM-dd HH:mm}")
                                     .FontSize(10)
                                     .FontColor(Colors.Grey.Darken1);
                            });

                            row.ConstantItem(80)
                               .Height(40)
                               .Background(Colors.Blue.Darken4)
                               .AlignCenter()
                               .AlignMiddle()
                               .Text("Medicare")
                               .FontColor(Colors.White)
                               .Bold();
                        });

                        col.Item()
                           .PaddingTop(8)
                           .BorderBottom(1)
                           .BorderColor(Colors.Blue.Darken4)
                           .Text("");
                    });

                    // Content
                    page.Content().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(25);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("المريض").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("الطبيب").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("التخصص").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("التاريخ").FontColor(Colors.White).Bold();
                            header.Cell().Background(Colors.Blue.Darken4).Padding(6)
                                  .Text("الحالة").FontColor(Colors.White).Bold();
                        });

                        for (int i = 0; i < appointments.Count; i++)
                        {
                            var a = appointments[i];
                            var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten3;

                            var statusColor = a.Status == "مكتمل"
                                ? Colors.Green.Darken2
                                : a.Status == "ملغي"
                                    ? Colors.Red.Darken2
                                    : Colors.Orange.Darken2;

                            table.Cell().Background(bg).Padding(5).Text((i + 1).ToString());
                            table.Cell().Background(bg).Padding(5).Text(a.Patient?.FullName ?? "");
                            table.Cell().Background(bg).Padding(5).Text(a.Doctor?.FullName ?? "");
                            table.Cell().Background(bg).Padding(5).Text(a.Doctor?.Specialization ?? "");
                            table.Cell().Background(bg).Padding(5).Text(a.AppointmentDate.ToString("yyyy-MM-dd HH:mm"));
                            table.Cell().Background(bg).Padding(5)
                                 .Text(a.Status ?? "").FontColor(statusColor);
                        }
                    });

                    // Footer
                    page.Footer().BorderTop(1).BorderColor(Colors.Grey.Lighten2).PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem()
                           .Text($"إجمالي: {appointments.Count}")
                           .FontSize(10)
                           .FontColor(Colors.Grey.Darken1);

                        row.RelativeItem()
                           .AlignRight()
                           .Text("نظام إدارة المستشفى — Medicare")
                           .FontSize(10)
                           .FontColor(Colors.Grey.Darken1);
                    });
                });
            }).GeneratePdf();
        }
    }
}