namespace LinearRegressionBackend
{
    // TODO: It could be removed. Optimizers are iterable. Estimators are not iteratable.
    /// <summary>
    /// Iteratable optimizer in order to find the arbitrary function local minimum.
    /// </summary>
    public interface IIterable
    {
        /// <summary>
        /// The parameter change rate during update.
        /// </summary>
        double LearningRate { get; set; }
    }
}
