using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;
using Itmo.ObjectOrientedProgramming.Lab3.MessageLogicImplementors.Users;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MessagesTests
{
    [Fact]
    public void MessageInitialIsCheckedIsFalse()
    {
        // Arrange
        var user = new User();
        Message message = Message.Build
                                .WithHeader("Greeting")
                                .WithBody("First message!")
                                .WithImportance("High")
                                .Build();

        // Act
        user.ProcessMessage(message);

        // Assert
        foreach (UserMessage messageI in user.MessageChecker.Messages)
        {
            Assert.False(messageI.IsChecked);
        }
    }

    [Fact]
    public void MessageCheckChangesStatus()
    {
        // Arrange
        var user = new User();
        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("High")
            .Build();

        // Act
        user.ProcessMessage(message);
        user.CheckMessage(message);

        // Assert
        foreach (UserMessage messageI in user.MessageChecker.Messages)
        {
            Assert.True(messageI.IsChecked);
        }
    }

    [Fact]
    public void CheckedMessageAfterRepeatedCheckReturnsFailure()
    {
        // Arrange
        var user = new User();
        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("High")
            .Build();

        // Act
        user.ProcessMessage(message);
        user.CheckMessage(message);

        // Assert
        Assert.True(user.CheckMessage(message) is CheckMessageResult.Failure);
    }

    [Fact]
    public void UnimportantMessageIsNotReceivedByAddressee()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();

        var user = new User();

        PriorityAddressee userWithImportance = PriorityAddressee.Build
            .WithImportance(new Importance("High"))
            .WithAddressee(user)
            .Build();

        LoggedAddressee loggedUser = LoggedAddressee.Build
            .WithLogger(mockLogger.Object)
            .WithAddressee(userWithImportance)
            .Build();

        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("Medium")
            .Build();

        // Act
        loggedUser.ProcessMessage(message);

        // Assert
        mockLogger.Verify(logger => logger.Log(It.Is<string>(s => s.Contains("is not received"))), Times.Once);
    }

    [Fact]
    public void ImportantMessageIsReceivedByAddressee()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();

        var user = new User();

        PriorityAddressee userWithImportance = PriorityAddressee.Build
            .WithImportance(new Importance("Medium"))
            .WithAddressee(user)
            .Build();

        LoggedAddressee loggedUser = LoggedAddressee.Build
            .WithLogger(mockLogger.Object)
            .WithAddressee(userWithImportance)
            .Build();

        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("High")
            .Build();

        // Act
        loggedUser.ProcessMessage(message);

        // Assert
        mockLogger.Verify(logger => logger.Log(It.Is<string>(s => s.Contains("is received"))), Times.Once);
    }

    [Fact]
    public void MessengerOutputsAsExpected()
    {
        // Arrange
        var mockedConsoleWriter = new Mock<IConsoleWriter>();
        Messenger messenger = Messenger.Build
            .WithConsoleWriter(mockedConsoleWriter.Object)
            .Build();
        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("High")
            .Build();

        // Act
        messenger.ProcessMessage(message);

        // Assert
        mockedConsoleWriter.Verify(writer => writer.WriteLine(It.Is<string>(s => s.Contains("messenger"))), Times.Once);
    }

    [Fact]
    public void UserReceivesMessangeWithPriorityOnce()
    {
        // Arrange
        var user = new User();
        PriorityAddressee userWithImportance = PriorityAddressee.Build
            .WithImportance(new Importance("High"))
            .WithAddressee(user)
            .Build();

        Group group = Group.Build
            .AddAddressee(user)
            .AddAddressee(userWithImportance)
            .Build();

        Message message = Message.Build
            .WithHeader("Greeting")
            .WithBody("First message!")
            .WithImportance("Medium")
            .Build();

        // Act
        group.ProcessMessage(message);

        // Assert
        Assert.Single(user.MessageChecker.Messages);
    }
}
