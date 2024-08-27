namespace ClockAppDemo
{
    public interface IMainScreenPresenter
    {
        public void ChangeToScreen(ScreenType screenType);
        public void DisableScreens();
    }
}
