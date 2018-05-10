using System;

namespace CircularBuffer
{
    public class ExponentialMovingAverage
    {
        private double _alpha;
        
        public double Average { get; set; }

        public ExponentialMovingAverage(double alpha)
        {
            _alpha = alpha;
            Average = double.NaN;
        }
        
        public void Add(double value) {
            if (double.IsNaN(Average))
            {
                Average = value;
            }

            Average += _alpha * (value - Average);
        }
    }
}