using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.TreeParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;
using Xunit;

namespace Lab4.Tests;

public class CommandParserTests
{
    private const string SampleDirectory = "sample_dir";
    private const string SampleFile = "sample_file";

    [Fact]
    public void ConnectParseTest()
    {
        // Arrange
        const string command = "connect " + SampleDirectory;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new ConnectParameterHandler(new ConnectedFileSystem(new LocalFileSystem())));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(LocalConnectCommand.Build
                .WithFileSystem(new ConnectedFileSystem(new LocalFileSystem()))
                .WithPath(SampleDirectory)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        LocalConnectCommand commandActual = Assert.IsType<ValidatedConnectCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void DisconnectParseTest()
    {
        // Arrange
        const string command = "disconnect";
        IParameterHandler handler = new BaseHandler()
            .AddNext(new DisconnectParameterHandler(new ConnectedFileSystem(new LocalFileSystem())));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = new DisconnectCommand(new ConnectedFileSystem(new LocalFileSystem()));

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        DisconnectCommand commandActual = Assert.IsType<ValidateDisconnectCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void TreeGotoParseTest()
    {
        const string command = "tree goto " + SampleDirectory;
        IParameterHandler handler = new BaseHandler()
        .AddNext(new TreeParameterHandler()
                .AddNextTreeParameterHandler(new TreeGotoParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(LocalTreeGotoCommand.Build
                .WithNewPath(SampleDirectory)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        LocalTreeGotoCommand commandActual = Assert.IsType<ValidateTreeGotoCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void TreeListParseTest()
    {
        const string command = "tree list -d 2";
        IParameterHandler handler = new BaseHandler()
            .AddNext(new TreeParameterHandler()
            .AddNextTreeParameterHandler(new TreeListParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(LocalTreeListCommand.Build
                .WithMaxDepth(2)
                .WithOutputMode("console")
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        LocalTreeListCommand commandActual = Assert.IsType<ValidateTreeListCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void FileShowParseTest()
    {
        const string command = "file show " + SampleFile;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new FileParameterHandler()
            .AddNextFileParameterHandler(new FileShowParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(FileShowCommand.Build
                .WithPath(SampleFile)
                .WithWriter(new ConsoleWriter())
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        FileShowCommand commandActual = Assert.IsType<ValidateFileShowCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void FileDeleteParseTest()
    {
        const string command = "file delete " + SampleFile;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new FileParameterHandler()
                .AddNextFileParameterHandler(new FileDeleteParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(FileDeleteCommand.Build
                .WithPath(SampleFile)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        FileDeleteCommand commandActual = Assert.IsType<ValidateFileDeleteCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void FileRenameParseTest()
    {
        const string newName = SampleFile + "(new)";
        const string command = "file rename " + SampleFile + " " + newName;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new FileParameterHandler()
                .AddNextFileParameterHandler(new FileRenameParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(FileRenameCommand.Build
                .WithPath(SampleFile)
                .WithNewFileName(newName)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        FileRenameCommand commandActual = Assert.IsType<ValidateFileRenameCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void FileMoveParseTest()
    {
        const string command = "file move " + SampleFile + " " + SampleDirectory;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new FileParameterHandler()
                .AddNextFileParameterHandler(new FileMoveParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(FileMoveCommand.Build
                .WithSourcePath(SampleFile)
                .WithTargetPath(SampleDirectory)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        FileMoveCommand commandActual = Assert.IsType<ValidateFileMoveCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }

    [Fact]
    public void FileCopyParseTest()
    {
        const string command = "file copy " + SampleFile + " " + SampleDirectory;
        IParameterHandler handler = new BaseHandler()
            .AddNext(new FileParameterHandler()
                .AddNextFileParameterHandler(new FileCopyParameterHandler()));
        IEnumerator<string> enumerator = command.Split(" ").ToList().GetEnumerator();

        IBasicCommand expectedCommand = Assert.IsType<CommandBuildResult.Success>(FileCopyCommand.Build
                .WithSourcePath(SampleFile)
                .WithTargetPath(SampleDirectory)
                .Build())
            .Command;

        // Act
        ParameterHandleResult? result = handler.Handle(enumerator);
        Assert.NotNull(result);
        ParameterHandleResult.Success success = Assert.IsType<ParameterHandleResult.Success>(result);
        FileCopyCommand commandActual = Assert.IsType<ValidateFileCopyCommand>(success.Command).Command;

        // Assert
        Assert.Equal(expectedCommand, commandActual);
    }
}
