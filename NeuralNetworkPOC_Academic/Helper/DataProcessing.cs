namespace NeuralNetworkPOC.Helper
{
    public static class DataProcessing
    {
        ///<summary>
        ///Normalize data (map from value to 0-1 )
        ///</summary>
        public static double Normalize(double value, double dataMaxValue, double dataMinValue)
        {
            return (value - dataMinValue) / (dataMaxValue - dataMinValue);
        }

        ///<summary>
        ///Denormalize data (map from 0-1 to value )
        ///</summary>
        public static double Denormalize(double normalizedValue, double dataMaxValue, double dataMinValue)
        {
            return normalizedValue * (dataMaxValue - dataMinValue) + dataMinValue;
        }

        ///<summary>
        ///Normalize data (map from value to 0-1 )
        ///</summary>
        public static double[] Normalize(double[] values, double dataMaxValue, double dataMinValue)
        {
            double[] result = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
                result[i] = Normalize(values[i], dataMaxValue, dataMinValue);

            return result;
        }

        ///<summary>
        ///Denormalize data (map from 0-1 to value )
        ///</summary>
        public static double[] Denormalize(double[] normalizedValues, double dataMaxValue, double dataMinValue)
        {
            double[] result = new double[normalizedValues.Length];

            for (int i = 0; i < normalizedValues.Length; i++)
                result[i] = Denormalize(normalizedValues[i], dataMaxValue, dataMinValue);

            return result;
        }
        
        ///<summary>
        ///Normalize data (map from value to NRangeMax-NRangeMin )
        ///</summary>
        public static double Normalize(double value, double dataMaxValue, double dataMinValue, double NRangeMax, double NRangeMin)
        {
            return (NRangeMax - NRangeMin) / (dataMaxValue - dataMinValue) * (value - dataMinValue) + NRangeMin;
        }

        ///<summary>
        ///Denormalize data (map from 0-1 to value )
        ///</summary>
        public static double Denormalize(double normalizedValue, double dataMaxValue, double dataMinValue, double NRangeMax, double NRangeMin)
        {
            return (normalizedValue - NRangeMin) * (dataMaxValue - dataMinValue) / (NRangeMax - NRangeMin) + dataMinValue;
        }

        ///<summary>
        ///Normalize data (map from value to 0-1 )
        ///</summary>
        public static double[] Normalize(double[] values, double dataMaxValue, double dataMinValue, double NRangeMax, double NRangeMin)
        {
            double[] result = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
                result[i] = Normalize(values[i], dataMaxValue, dataMinValue, NRangeMax, NRangeMin);

            return result;
        }

        ///<summary>
        ///Denormalize data (map from 0-1 to value )
        ///</summary>
        public static double[] Denormalize(double[] normalizedValues, double dataMaxValue, double dataMinValue, double NRangeMax, double NRangeMin)
        {
            double[] result = new double[normalizedValues.Length];

            for (int i = 0; i < normalizedValues.Length; i++)
                result[i] = Denormalize(normalizedValues[i], dataMaxValue, dataMinValue, NRangeMax, NRangeMin);

            return result;
        }

        public static bool IsNumberType(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

    }
}
