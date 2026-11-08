namespace Task_5
{
    enum QuestionLevel
    {
        Easy,Medium,Hard
    }

    //Parent Class(Base Class)
    class Question
    {
        public string Header { get; set; }
        public int Marks { get; set; }
        public QuestionLevel Level { get; set; }

        public virtual void Display()
        {
            Console.WriteLine($"Question: {Header}");
        }

        public virtual bool CheckAnswer(string answer)
        {
            return false;
        }
    }

    //Child Classes

    /*
     * in class TrueFalseQuestion : I do property named CorrectAnswer (bool) because student will choose true or false 
     * used TryParse to convert string value to an integer and ckeck if the convertion succeeds without throwing an exception.
     */
    class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer {  get; set; }
        public override void Display()
        {
            base.Display();
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }

        public override bool CheckAnswer(string answer)
        {
            if(!int.TryParse(answer,out int studentAnswer))
                return false;

            if(studentAnswer ==  1)
                return CorrectAnswer == true;

            if (studentAnswer == 2)
                return CorrectAnswer == false;


            return false;

        }
    }

    /*
     * in class ChooseOneQuestion : used property named Choices as array  to receive choices then Choices are 4 
     * used for loop in Display Method to dispaly these choices and used TryParse too.
     */
    class ChooseOneQuestion : Question
    {
        public string[] Choices {  get; set; }
        public int CorrectAnswer { get; set; }

        public override void Display()
        {
            base.Display(); 
            for(int i=0;i<Choices.Length;i++ )
            {
                Console.WriteLine($"{i+1}.{Choices[i]}");
            }
        }

        public override bool CheckAnswer(string answer)
        {
            if(Choices == null || Choices.Length !=4)
                return false;
            if (!int.TryParse(answer, out int studentAnswer))
                return false;

            if (studentAnswer < 1 || studentAnswer > 4)
                return false;


            return studentAnswer == CorrectAnswer;

        }
    }

    /*
     * in class MultipleChoiceQuestion : used property named choices as array to receive choices then choices are 4.
     * used property named correctanswer as array because student will choice multiple choices from 4 choices.
     * used Dispaly() such as chooseoneanswer without changing
     * 
      */
    class MultipleChoiceQuestion : Question
    {
        public string[] Choices { get; set; }
        public int[] CorrectAnswers { get; set; }


        public override void Display()
        {
            base.Display();
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}.{Choices[i]}");
            }
        }


        public override bool CheckAnswer(string answer)
        {
            //to ensure from finding choices(validation)
            if (Choices == null || Choices.Length != 4)
                return false;

            //to ensure from finding correct answers(doctor determin correct answers)
            if(CorrectAnswers == null || CorrectAnswers.Length == 0)
                return false;

            //used split() to divide sring (output) to parts by spicial interval
            string[] parts=answer.Split(',');

            //to store student's choices 
            List<int> studentAnswers = new List<int>();

            //loop through all student answers ,convert each answer from string to int using TryParse.
            for (int i = 0; i < parts.Length; i++)
            {
               if(!int.TryParse(parts[i], out int choice)) 
                    return false;

                if (choice < 1 || choice > 4)
                    return false;

                //prevent the student from choiceing the same choice more than once 
                if(studentAnswers.Contains(choice))
                    return false;

                //Add each valid answer to list
                studentAnswers.Add(choice);
            }

            for(int i = 0;i < CorrectAnswers.Length;i++)
            {
                //compares student's answers with correct answers 
                if(!studentAnswers.Contains(CorrectAnswers[i]))
                    return false;
            }

             return true;
        }

        }

    internal class Program
    {
        //place to store questions 
       static List<Question> questionBank = new List<Question>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=====Examination System======");
                Console.WriteLine("1.Doctor Mode");
                Console.WriteLine("2.Student Mode");
                Console.WriteLine("3.Exit");
                Console.Write("Choose an option: ");
                string choice=Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DoctorMode();
                        break;
                    case "2":
                        StudentMode();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Input .Please choose from 1 to 3 ");
                        break;
                }

            }
        }

        //used TryParse to if user write word not number ,progrm will have Exception so I used it (in system).

        /*
         * in Method named DoctorMode => Doctor add Questions to Question Bank that select count of Questions,select Question type 
         * ((True/False) || (Chooseone) || (Multiple Choice) ) , select Question Level ,Question Header ,Mark of Question 
         * and Correct Answer to Question 
         */

        static void DoctorMode()
        {
            Console.WriteLine("=======Doctor Mode=======");
            Console.Write("How many questions do you want to add?: ");
            string input = Console.ReadLine();
           
            if(!int.TryParse(input, out int numberQuestion) || numberQuestion <=0 )
            {
                Console.WriteLine("Invalid number or enter positive number");
                return;
            }

            
              for (int i = 0; i < numberQuestion; i++)
              {
                int numberQuestiontype;
                while (true)
                {
                    Console.WriteLine("Choose Question Type: ");
                    Console.WriteLine("1.True / False");
                    Console.WriteLine("2.Choose One");
                    Console.WriteLine("3.Multiple Choice");
                    string inputype = Console.ReadLine();
                    if (int.TryParse(inputype, out numberQuestiontype) && numberQuestiontype >= 1 && numberQuestiontype <= 3)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid question type.");
                }
                   

                Question question = null;
                switch (numberQuestiontype)
                {
                    case 1:
                        /*used TrueFalseQuestion class to create a new question and assign it to the question variable.
                         * used TryParse to convert the correct answer input from string to integer and check if it is valid (1 or 2).
                         * used if statement to assign the correct answer based on the user's input.
                         */
                        TrueFalseQuestion tfQuestion =new TrueFalseQuestion();
                        question=tfQuestion;
                        int correctAnswer;
                        while (true)
                        {
                            Console.WriteLine("Enter Correct Answer: ");
                            Console.WriteLine("1.True");
                            Console.WriteLine("2.False");
                            string answerInput = Console.ReadLine();

                            if (int.TryParse(answerInput, out correctAnswer) && (correctAnswer == 1 || correctAnswer == 2))
                            {
                                //if CorrectAnswer ==1 => result true , if CorrectAnswer ==2 => result false
                                tfQuestion.CorrectAnswer = (correctAnswer == 1);
                                break;
                            }
                                Console.WriteLine("Invalid answer");
                                
                        }
                        
                        break;
                    case 2:
                        /*used ChooseOneQuestion class to create a new question and assign it to the question variable.
                         * used for loop to  enter 4 choices for the question and store them in the Choices array of the ChooseOneQuestion object.
                         * used TryParse to convert the correct answer input from string to integer and check if it is valid (between 1 and 4).
                         */
                        ChooseOneQuestion coQuestion=new ChooseOneQuestion();
                        question=coQuestion;
                        coQuestion.Choices = new string[4];
                        for(int j=0;j < coQuestion.Choices.Length;j++)
                        {
                            Console.WriteLine($"Enter Choice {j + 1}: ");
                            coQuestion.Choices[j] = Console.ReadLine();
                        }

                        int correctChoice;
                        while(true)
                        {
                            Console.WriteLine("Enetr Correct Answer (1-4): ");
                            string correctInput = Console.ReadLine();
                            if (int.TryParse(correctInput, out correctChoice) && correctChoice >= 1 && correctChoice <= 4)
                            {
                                coQuestion.CorrectAnswer = correctChoice;
                                break;
                            }
                                Console.WriteLine("Invalid correct answer.");     
                            
                        }
                       
                        break;
                    case 3:
                        /*used MultipleChoiceQuestion class to create a new question and assign it to the question variable.
                         * used for loop to  enter 4 choices for the question and store them in the Choices array of the MultipleChoiceQuestion object.
                         * used Split() to divide string (output) to parts by special interval (comma).
                         * used TryParse to convert each correct answer input from string to integer and check if it is valid (between 1 and 4).
                         * used List<int> to store correct answers and prevent duplicates.
                         */
                        MultipleChoiceQuestion mcQuestion = new MultipleChoiceQuestion();
                        question = mcQuestion;

                        // Enter 4 choices
                        mcQuestion.Choices = new string[4];

                        for (int j = 0; j < mcQuestion.Choices.Length; j++)
                        {
                            Console.Write($"Enter Choice {j + 1}: ");
                            mcQuestion.Choices[j] = Console.ReadLine();
                        }

                        // Enter correct answers
                        bool isValid = false;

                        while (!isValid)
                        {
                            Console.Write("Enter Correct Answers (Example: 1,3): ");

                            string correctMultiInput = Console.ReadLine();

                            string[] parts = correctMultiInput.Split(',');

                            List<int> correctAnswersList = new List<int>();

                            isValid = true;

                            for (int j = 0; j < parts.Length; j++)
                            {
                                if (!int.TryParse(parts[j].Trim(), out int correctMultiAnswer) ||
                                    correctMultiAnswer < 1 || correctMultiAnswer > 4)
                                {
                                    isValid = false;
                                    break;
                                }

                                if (correctAnswersList.Contains(correctMultiAnswer))
                                {
                                    isValid = false;
                                    break;
                                }

                                correctAnswersList.Add(correctMultiAnswer);
                            }

                            if (correctAnswersList.Count == 0)
                            {
                                isValid = false;
                            }

                            if (!isValid)
                            {
                                Console.WriteLine("Invalid answers. Please try again.");
                            }
                            else
                            {
                                mcQuestion.CorrectAnswers = correctAnswersList.ToArray();
                            }
                        }

                        break;

                }


                //Header
                while (true)
                {
                    Console.Write("Enter your Question: ");
                    string inputHeader = Console.ReadLine();
                    if (inputHeader != null && inputHeader.Length > 0)
                    {
                        question.Header = inputHeader;
                        break;
                    }
                    Console.WriteLine("Question header cannot be empty");
                   
                }
              

                /*
                 * used while loop to ensure that the user enters a valid positive number for the marks of the question.
                 * used TryParse to convert the input from string to integer and check if it is valid (greater than 0).
                 * if valid, assign it to the Marks property  and exit the loop.
                 * if invalid, display an error message and user to enter the marks again.
                 */
                //Mark
                int mark = 0;
                bool validMark = false;

                while (!validMark)
                {
                    Console.Write("Enter Mark of Question: ");

                    string inputMark = Console.ReadLine();

                    if (int.TryParse(inputMark, out mark) && mark > 0)
                    {
                        question.Marks = mark;
                        validMark = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid marks. Please enter a positive number.");
                    }
                }

                //level
                int Levelchoice;
                while (true)
                {
                    Console.WriteLine("Choose Question Level: ");
                    Console.WriteLine("1.Easy");
                    Console.WriteLine("2.Medium");
                    Console.WriteLine("3.Hard");
                    string inputLevel = Console.ReadLine();
                    if (int.TryParse(inputLevel, out Levelchoice) && Levelchoice >= 1 && Levelchoice <= 3)
                    {
                        //Doctor's choices start from 1 and Enum start from 0
                        question.Level = (QuestionLevel)(Levelchoice - 1);
                        break;
                    }
                    Console.WriteLine("Invalid question level.");
                }
                
                //Add the question to the question bank
                questionBank.Add(question);
                Console.WriteLine("Question added successfully!");
            }
           


        }

        /* 
         * in Method named StudentMode => student choose Exam Type(Partical || Final),choose Question Level (Easy || Medium || Hard)
         * and dislay result mark to exam 
         */

        static void StudentMode()
        {
            Console.WriteLine("Choose Exam Type: ");
            Console.WriteLine("1.Partical ");
            Console.WriteLine("2.Final ");
            int exam_type;
            while (true)
            {
                string examType = Console.ReadLine();
                if (int.TryParse(examType, out exam_type) && (exam_type == 1 || exam_type == 2))
                {
                    break;
                }
                Console.WriteLine("Invalid input");
            }
            Console.WriteLine("Choose Question Level: ");
            Console.WriteLine("1.Easy ");
            Console.WriteLine("2.Medium ");
            Console.WriteLine("3.Hard ");
            QuestionLevel selectedLevel;
            while (true)
            {
                string levelChoice = Console.ReadLine();
                if (int.TryParse(levelChoice, out int level_choice) && (level_choice >= 1 && level_choice <= 3))
                {
                    selectedLevel = (QuestionLevel)(level_choice - 1);
                    break;
                }
                Console.WriteLine("Invalid input");
                
            }

            //list to put Questions level that equal student's Choices
            List<Question> selectedQuestions = new List<Question>();
            for(int i=0;i< questionBank.Count;i++)
            {
                if (questionBank[i].Level == selectedLevel)
                {
                    selectedQuestions.Add(questionBank[i]);
                }
            }

            //if question level (Easy || Medium || Hard) is not found 
            if (selectedQuestions.Count == 0)
            {
                Console.WriteLine("No questions avaliable for this level.");
                return;
            }
            //select count of Questions that appear to student
            int questionsDisplay;
            //if exam_type == 1 => partical //if exam_type == 2 => final
            if (exam_type == 1)
            {
                questionsDisplay = selectedQuestions.Count / 2;
                //problem notfinding enough Questions(Partical) 
                if (questionsDisplay == 0)
                {
                    Console.WriteLine("Not Enough questions for Partical Exam");
                    return;
                }
            }
            else
            {
                questionsDisplay = selectedQuestions.Count;
            }

            int totalMarks = 0;
            int studentMarks = 0;

            for(int j=0; j < questionsDisplay; j++)
            {
                Question currentQuestion = selectedQuestions[j];
                Console.WriteLine($"Question {j + 1}: ");
                currentQuestion.Display();
                Console.Write("Enter your answer: ");
                string studentAnswer = Console.ReadLine();
                totalMarks += currentQuestion.Marks;
                bool isCorrect = currentQuestion.CheckAnswer(studentAnswer);
                if (isCorrect)
                {
                    studentMarks += currentQuestion.Marks;
                    Console.WriteLine("Correct Answer!");
                }
                else
                {
                    Console.WriteLine("Wrong Answer!");
                }
            }

            Console.WriteLine("=========Exam Finished============");
            Console.WriteLine($"Your Result: {studentMarks} / {totalMarks}");

           



        }

        
    }
}
