using System;
using System.Collections.Generic;

namespace DDDSample.Domain.Members.Entities;

public class Member
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Mail { get; private set; }

    public Member(string name, string mail)
    {
        Name = name;
        Mail = mail;
    }

    public void Update(string name, string mail)
    {
        Name = name;
        Mail = mail;
    }
}
