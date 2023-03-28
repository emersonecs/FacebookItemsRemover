using Emgu.CV.CvEnum;
using Emgu.CV;
using Macro.Services;

Thread.Sleep(800);
Mat menuButton = CvInvoke.Imread("./img/menuButton.png");
Mat unsaveButton = CvInvoke.Imread("./img/unsaveButton.png");

while (!Console.KeyAvailable)
{
    var printScreen = ScreenService.Capture();

    CvInvoke.CvtColor(printScreen, printScreen, ColorConversion.Bgra2Bgr);
    CvInvoke.CvtColor(menuButton, menuButton, ColorConversion.Bgra2Bgr);
    CvInvoke.CvtColor(unsaveButton, unsaveButton, ColorConversion.Bgra2Bgr);

    var points = ImageService.Recognize(printScreen, menuButton);

    foreach (var point in points.Where(point => !point.IsEmpty))
    {
        MouseService.MoveAndLeftClick(point);
        Thread.Sleep(800);

        printScreen = ScreenService.Capture();
        CvInvoke.CvtColor(printScreen, printScreen, ColorConversion.Bgra2Bgr);

        var pointsUnsaveButton = ImageService.Recognize(printScreen, unsaveButton).FirstOrDefault();
        if (!pointsUnsaveButton.IsEmpty)
            MouseService.MoveAndLeftClick(pointsUnsaveButton);
    }
}