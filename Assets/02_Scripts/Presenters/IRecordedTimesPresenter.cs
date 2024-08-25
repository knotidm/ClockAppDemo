namespace ClockAppDemo
{
    public interface IRecordedTimesPresenter
    {
        public void AddRecordToList(long lapTime, long totalTime);

        public void ClearRecordedTimesPanel();
    }
}
