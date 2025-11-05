import sys
import NXOpen.Drawings
import NXOpen.Drafting

def main():
    #nhận đường dẫn Part/Assembly và các tham số khác
    model_path = sys.argv[1]
    sheet_name = sys.argv[2]
    view_name = sys.argv[3]
    view_type = sys.argv[4].lower()
    x = float(sys.argv[5])
    y = float(sys.argv[6])
    scale = float(sys.argv[7])

    session = NXOpen.Session.GetSession()

    #mở part/assembly nếu chưa mở
    parts = session.Parts
    model_part= None

    #kiểm tra part đã mở chưa
    for p in parts.All:
        if p.FullPathName.lower() == model_path.lower():
            model_part = p
            break

    if not model_part:
        part_load_status = NXOpen.PartLoadStatus()
        model_part = parts.OpenBase(model_path, out_partLoadStatus=part_load_status)
        del part_load_status

    #lấy sheet theo tên
    sheet = model_part.DrawingSheets.FindObject(sheet_name)

    #tạo base view builder
    base_view_builder = model_part.DraftingViews.CreateBaseViewBuilder(None)
    base_view_builder.Placement.Sheet = sheet
    base_view_builder.Placement.PointOnSheet = NXOpen.Point2d(x, y)
    base_view_builder.ModelName = model_part.Name
    base_view_builder.Scale = scale
    base_view_builder.ViewName = view_name

    directions ={
        "top": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Top,
        "left": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Left,
        "right": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Right,
        "front": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Front,
        "back": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Back,
        "bottom": NXOpen.Drawings.BaseViewBuilder.ViewDirection.Bottom,
        "iso": NXOpen.Drawings.BaseViewBuilder.ViewDirection.IsoTopRight,
        "isometric": NXOpen.Drawings.BaseViewBuilder.ViewDirection.IsoTopRight,
    }

    base_view_builder.FrontViewDirection = directions.get(view_type, NXOpen.Drawings.BaseViewBuilder.ViewDirection.Top)

    base_view_builder.Commit()
    base_view_builder.Destroy()

    lw = session.ListingWindow
    lw.Open()
    lw.WriteLine(f"Đã tạo base view {view_name} ({view_type}) tại ({x}, {y}) trên sheet {sheet_name} của file {model_path}")


if __name__=="__main__":
    main()
