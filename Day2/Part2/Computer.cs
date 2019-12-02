using System;
using System.Collections.Generic;

namespace Day2
{
    public class Computer
    {
        private List<int> memory;
        private int counter = 0;
        public List<int> Memory => memory;
        public int ProgramCounter => counter;
        public Computer(List<int> input)
        {
            memory = new List<int>(input);
        }

        public void Run()
        {
            while(Process());
        }

        bool Process()
        {
            switch(memory[counter])
            {
                case 99:
                    return false;

                case 1:
                    Add();
                    break;

                case 2: 
                    Multiply();
                    break;
                
                default:
                    throw new InvalidOperationException("Unknown OPcode!");
            }

            return true;
        }

        void Add()
        {
            memory[memory[counter + 3]] = memory[memory[counter + 1]] + memory[memory[counter + 2]];
            counter += 4;
        }

        void Multiply()
        {
            memory[memory[counter + 3]] = memory[memory[counter + 1]] * memory[memory[counter + 2]];
            counter += 4;
        }
    }
}