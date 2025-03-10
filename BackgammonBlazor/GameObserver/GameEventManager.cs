namespace BackgammonBlazor.GameObserver
{
    public class GameEventManager
    {
        private readonly List<Action> _observers = new List<Action>();

        public void Subscribe(Action observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(Action observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer?.Invoke();
            }
        }
    }
}
