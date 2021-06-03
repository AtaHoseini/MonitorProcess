using System.Diagnostics;
using System.Threading;
using Xunit;

namespace monitor.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            const int MaxLifeDurationInMinutes = 1;
            //Arrange
            var notepadProcess = Process.Start("notepad.exe");

            Thread.Sleep(MaxLifeDurationInMinutes*60*1000);
            //Act
            var killProcessService = new KillProcessService();


            var result=killProcessService.Execute(MaxLifeDurationInMinutes, notepadProcess);

            Process[] listOfProccessWithNotepadName = Process.GetProcessesByName("notepad");

            var actualResult = listOfProccessWithNotepadName is null ? 0 : listOfProccessWithNotepadName.Length;
            var expectedResult = 0;
            
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Test2()
        {
            const int MaxLifeDurationInMinutes = 5;
            //Arrange
            using var notepadProcess = Process.Start("notepad.exe");
            Thread.Sleep(MaxLifeDurationInMinutes *60* 1000);
         
            //Act
            //var killProcessService = new KillProcessService();
            //killProcessService.Execute(MaxLifeDurationInMinutes, notepadProcess);
            Process[] listOfProccessWithNotepadName = Process.GetProcessesByName(notepadProcess.ProcessName);
            var actualResult = listOfProccessWithNotepadName is null ? 0 : listOfProccessWithNotepadName.Length;
            
            //Assert
            Assert.True(actualResult  > 0);
        }
    }
}
