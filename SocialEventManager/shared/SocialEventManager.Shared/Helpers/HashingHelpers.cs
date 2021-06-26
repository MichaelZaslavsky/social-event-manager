namespace SocialEventManager.Shared.Helpers
{
    public static class HashingHelpers
    {
        /// <summary>
        /// This is a simple hashing function from Robert Sedgwicks Hashing in C book.
        /// Also, some simple optimizations to the algorithm in order to speed up
        /// its hashing process have been added. from: www.partow.net.
        /// </summary>
        /// <param name="input">array of objects, parameters combination that you need
        /// to get a unique hash code for them.</param>
        /// <returns>Hash code.</returns>
        public static int RSHash(params object[] input)
        {
            const int b = 378551;
            int a = 63689;
            int hash = 0;

            unchecked
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != null)
                    {
                        hash = (hash * a) + input[i].GetHashCode();
                        a *= b;
                    }
                }
            }

            return hash;
        }
    }
}
