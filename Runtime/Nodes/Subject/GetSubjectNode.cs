using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel.Subject
{
    public class GetSubjectNode : Node
    {
        [Input] public string SubjectId;

        [Output] public ReelSubject Subject;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Subject")
            {
                if(ReelDirector.Instance != null)
                {
                    return ReelDirector.Instance.GetSubjectFromId(SubjectId);

                }
            }

            return null;
        }
    }
}