//
// File: "TimePair.cs"
//
// Revision History: 
// 2024/04/12:TomislavMatas: Version "1.123.4.0"
// * Initial version.
//

namespace EZAsesAutoType
{
    internal class TimePair
    {
        public string? PunchIn = null;
        public string? PunchOut = null;

        /// <summary>
        /// Init instance.
        /// </summary>
        /// <param name="punchIn"></param>
        /// <param name="punchOut"></param>
        private void Initialize(string? punchIn, string? punchOut)
        {
            this.PunchIn = punchIn;
            this.PunchOut = punchOut;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TimePair()
        {
            this.Initialize(null, null);
        }

        /// <summary>
        /// Custom constructor.
        /// </summary>
        public TimePair(string? punchIn = null, string? punchOut = null)
        {
            this.Initialize(punchIn, punchOut);
        }

    } // class

} // namespace
