﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Quiz.Dto.Tag
{
    public class GetTagQuestionCount
    {
        public string TagText { get; set; }
        public int QuestionCount { get; set; }
    }
}
