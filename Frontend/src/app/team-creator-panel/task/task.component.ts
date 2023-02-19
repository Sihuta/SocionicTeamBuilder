import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NewTaskComponent } from './new-task/new-task.component';
import { Task } from './task';
import { TaskService } from './task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  tasks: Task[] = [];

  constructor(
    private readonly router: Router,
    private readonly service: TaskService,
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this.service.getTasks().subscribe(res => this.tasks = res), () => this.router.navigate(['']);
  }

  showTeamMembers($event: any) {
    let taskId = $event.target.name;
    localStorage.setItem('taskId', taskId);
    this.router.navigate(['/team-creator-panel/task/team-member']);
  }

  displayTeams($event: any) {
    let taskId = $event.target.name;
    localStorage.setItem('taskId', taskId);

    let task = this.tasks.find(t => t.id == taskId);
    if (task) {
      let timeIsLimited = task.timeIsLimited + '';
      localStorage.setItem('timeIsLimited', timeIsLimited);
    }

    this.router.navigate(['/team-creator-panel/task/team']);
  }

  delete($event: any) {
    let id = $event.target.name;
    this.service.deleteTask(id).subscribe(res => console.log(res));
    
    this.ngOnInit();
  }

  add() {
    this.router.navigate(['/team-creator-panel/task/new']);
  }
}
