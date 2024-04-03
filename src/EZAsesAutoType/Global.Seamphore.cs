//
// File: "Global.Seamphore.cs"
//
// Summary:
// Project specific semaphore implementations.
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZAsesAutoType
{
    internal static partial class Global
    {

        private static object m_LockCancelRequested = new object();
        private static bool m_CancelRequested = false;
        private static bool CancelRequested
        {
            get
            {
                lock (m_LockCancelRequested)
                {
                    return m_CancelRequested;
                }
            }
            set
            {
                lock (m_LockCancelRequested)
                {
                    m_CancelRequested = value;
                }
            }
        }
        public static bool GetCancelRequested()
        {
            return CancelRequested;
        }
        public static void SetCancelRequested(bool flag)
        {
            CancelRequested = flag;
        }

    } // class

} // namespace
