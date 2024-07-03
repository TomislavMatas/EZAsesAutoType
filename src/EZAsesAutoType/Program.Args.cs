//
// File: "Program.Args.cs"
//
// Revision History:
// 2024/07/03:TomislavMatas: Version "1.126.2"
// * Initial version.
//

namespace EZAsesAutoType
{
    /// <summary>
    ///  Program command line argument helper implementation.
    /// </summary>
    internal static partial class Program
    {
        private static string[]? m_Args = null;
        private static string[]? Args
        {
            get
            {
                if (m_Args == null)
                    m_Args = [];
                return m_Args;
            }
            set
            {
                m_Args = value;
            }
        }
        private static string[]? SetArgs(string[]? args)
        {
            string[]? prev = GetArgs();
            Args = args;
            return prev;
        }
        public static string[]? GetArgs()
        {
            return Args;
        }

        /// <summary>
        /// Check if argArray contains arg.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static bool ArgArrayContainsArg(string[]? args, string arg)
        {
            if (args == null)
                return false;

            if (arg == null)
                return false;

            foreach (string item in args)
                if (!string.IsNullOrEmpty(item))
                    if (item.Equals(arg, StringComparison.OrdinalIgnoreCase))
                        return true;

            return false;
        }

        public static bool IsArgProvided(string arg)
        {
            return ArgArrayContainsArg(GetArgs(), arg);
        }

    } // class

} // namespace
