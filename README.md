#Dependency Kata in CSharp#

##Overview

This kata takes the developer through the process of refactoring C# code with a high level of different dependencies for better testability.

##Prerequisites

###Visual Studio 2012 or 2013

The solution was created with VS2013 but should be compatible with VS2012

##Recommendations

###An automated test runner (prefer NCrunch)
Manually running tests during this process may impede the ability to complete the kata in a reasonable amount of time.

## Commits / Kata Outline

You can check out any point of the tutorial using
    git checkout step-?

To see the changes which between any two lessons use the git diff command.
    git diff step-?..step-?

###Step 0

Initial state of kata with a broken integration test. Start here if this is your first time.

Make the integration test pass by abstracting the breaking dependency on **Console.Readline()**. 


- Extract a public method named **GetInput()** that performs this action.
- Extract a class named **ConsoleAdapter** with the new method.
- Generate a new interface named **IConsoleAdapter** from the new class.
- Create a new constructor for **DoItAll** that accepts an **IConsoleAdapter** and sets a private member variable with it.
- Update all dependencies.
- Pull down a faking framework to the test project from NuGet (I'm using NSubstitute).
- In **DoItAll_Does_ItAll()** create a fake for **IConsoleAdapter** and pass it to the constructor of **DoItAll**.
- Revel in your moment of glory and move on to step 1.

###Step 1

The initial integration test should now be passing. 

We don't have coverage of a good portion of the code yet due to some conditions that haven't been met in this test. Also, we've abstracted the calls to Console that were breaking the test but still need to do a little more work to abstract it completely.

- Add an Assert.AreEqual to the test **DoItAll_Does_ItAll()** with an expected value of "The passwords don't match". This should cause it to fail again.
- Refactor **DoItAll.Do()** to return a string and return whatever is being logged. Should cause the test to pass again. Now we can write another meaningful integration test.
- Copy the existing test and paste it and rename it **DoItAll_Does_ItAll_MatchingPasswords()**.
- Change the expected value to "Database.SaveToLog Exception:" and update your fake so that **GetInput()** returns "something".
- Update the Assert to a StringAssert.Contains. Test should pass.
 
We now have reasonable minimal coverage and can start doing a bit more refactoring.

- As we did with **Console.Readline()** extract **Console.Writeline()** to a method named **SetOutput()** in the class and interface.
- Replace the rest of the console dependencies with this 
- Now that I'm looking at it I don't like the name of **IConsoleAdapter** as it indicated the dependency on Console. Let's rename it to **IOutputInputAdapter**. We will want to rename it's variable in **DoItAll** as well.

You have now concluded this part of the exercise. Please move on to the next step.

###Step 3

We now have 2 passing integration tests and all Console dependencies are abstracted.

- It looks like **DoItAll.Do()** might be trying to accomplish too many things as once. Let's extract the code in the try/catch block to a new method called **LogMessage()**.
- **LogMessage** looks so useful we might want to reuse it somewhere else. Let's extract it to a new class called **Logging**.
- We don't want to depend on this concretion, so lets extract an interface called **ILogging**.
- Now that I look at I think **Logging** is a bad class name. Change it to **DatabaseLogging**.
- Update **DoItAll**'s constructor to accept an **ILogging** and update the now failing integration tests.
- Our second integration test is breaking now... looks like the fake we created for **ILogging** broke our dependency on the implementation-specific details of that piece. Let's create a new test as a copy of this test and let our original test depend on the **DataBaselogging** concretion.
- In the new copy rename it to **DoItAll_Does_ItAll_MockLogging()** and replace the expected value with string.Empty. We can also remove the **Category("Integration")** attribute because this is an actual unit test!
