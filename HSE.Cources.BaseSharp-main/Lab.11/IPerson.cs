using System;

namespace Lab._11
{
    public interface IPerson : IComparable
    {
        IPerson GetSelfPerson { get; }

        Person BasePerson { get; }
        string Return_SeName();

        string Return_Name();

        void Input();

        void Show();

        new int CompareTo(object other);

        IPerson Create(IPerson person);
    }
}