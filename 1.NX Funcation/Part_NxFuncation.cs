using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using NXOpen;
using NXOpenUI;
using General_NXFuncations;

namespace Part_NxFuncation
{
    class Part_NxFuncation
    {
        public void CreatePoint()
        {
            try
            {
                // Lấy session NX hiện tại
                Session nxSession = NXOpen.Session.GetSession();

                // Đảm bảo NX đã có Part đang hoạt động
                Part workPart = nxSession.Parts.Work;
                if (workPart == null)
                {
                    Console.WriteLine("Không có file Part nào đang hoạt động.");
                    return;
                }

                Point3d coorsds = new Point3d(3, 5, 9);
                PointCollection pointFactory = workPart.Points;
                var myPoint = pointFactory.CreatePoint(coorsds);

                Point3d p1 = new Point3d(1, 6, 5);
                Point3d p2 = new Point3d(3, 2, 7);

                CurveCollection cureFactory = workPart.Curves;

                var myLine = cureFactory.CreateLine(p1, p2);
                string[] text = new[] { "Height", "Diameter", "Cost" };
                Point3d origin = new Point3d(3, 8, 0);
                AxisOrientation horiz = AxisOrientation.Horizontal;
                NXOpen.Annotations.AnnotationManager noteFactory;
                noteFactory = workPart.Annotations;
                var myNote = noteFactory.CreateNote(text, origin, horiz, null, null);
            }
            catch
            {

            }

        }

        private void gtr()
        {

            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;

            // Lấy DatumCollection từ Part
            DatumCollection datumCollection = workPart.Datums;

            // Xác định hai điểm 3D
            Point3d startPoint = new Point3d(0.0, 0.0, 0.0);
            Point3d endPoint = new Point3d(100.0, 0.0, 0.0);

            // Tạo Datum Axis cố định
            DatumAxis datumAxis = datumCollection.CreateFixedDatumAxis(startPoint, endPoint);

            // Hiển thị thông báo
            theSession.ListingWindow.Open();
            theSession.ListingWindow.WriteLine("Created Datum Axis from " + startPoint.ToString() + " to " + endPoint.ToString());
        }
    }
}
