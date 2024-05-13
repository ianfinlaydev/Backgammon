using BackgammonBlazor.Models;

namespace BackgammonBlazor.Helpers
{
    public static class CssHelper
    {
        //TODO: Move to css nth element?
        public static (string checkerClass, string checkerStyle) GetCss(this CheckerModel checker, int offset)
        {
            string checkerClass = checker.CheckerColor == CheckerColor.Light ? "checker-light" : "checker-dark";

            string checkerStyle = $"{(checker.Point.IsTopPoint() ? "top" : "bottom")}: {offset * 50}px";

            return (checkerClass, checkerStyle);
        }

        //TODO: Move to css nth element?
        public static (string pointClass, string pointStyle) GetCss(this BoardPointModel point)
        {
            string pointClass = point.IsTopPoint() ?
                (point.IsLightPoint() ? "top-light" : "top-dark") :
                (point.IsLightPoint() ? "bottom-light" : "bottom-dark");

            string pointStyle = $"{(point.IsTopPoint() ? "top: " : "bottom: ")}0";

            return (pointClass, pointStyle);
        }
    }
}
