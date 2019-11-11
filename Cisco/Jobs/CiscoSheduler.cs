using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiscoService.Jobs;
namespace CiscoService.Jobs {
    public static class CiscoSheduler {
        public static async void Start(int callerId, string callerNumber, string callerPassword)
        {
            Caller caller = Caller.Instance();
            caller.CallerId = callerId.ToString();
            caller.CallerNumber = callerNumber.ToString();
            caller.CallerPassword = callerPassword.ToString();
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ReapetedTask>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                //для каждого телефона нужен свои группа и триггер. При одинаковых триггирах и группах вызывает исключение что такой таск уже запущен.
                .WithIdentity("trigger"+ callerId.ToString(), "group"+ callerId.ToString())     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInSeconds(4)          // через 4 секунлы
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}
