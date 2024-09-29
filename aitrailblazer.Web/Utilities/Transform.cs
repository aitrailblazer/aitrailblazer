namespace aitrailblazer.net.Utilities
{
    public static class Transform
    {
        /// <summary>
        /// Transforms a temperature value into a topP value.
        /// </summary>
        /// <param name="value">The temperature value to transform.</param>
        /// <returns>The transformed topP value.</returns>
        public static double TransformToTopP(double value)
        {
            return value switch
            {
                0.1 => 0.5,
                0.2 => 0.6,
                0.3 => 0.7,
                0.4 => 0.75,
                0.5 => 0.8,
                0.6 => 0.85,
                0.7 => 0.9,
                0.8 => 0.92,
                0.9 => 0.95,
                1.0 => 1.0,
                _ => 0.6 // Default case, adjust this as needed based on your requirements
            };
        }

        // Add other utility methods here if needed
    }
}
