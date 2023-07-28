import {AfterViewInit, Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {Course} from './model/course';
import {Observable} from 'rxjs';
import { CoursesService } from './services/courses.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  courses$ : Observable<Course[]>;
  

  constructor(private coursesService: CoursesService) {

  }

  ngOnInit() {

    this.courses$ = this.coursesService.loadCourses();
  }

  save(course:Course) {
    this.coursesService.saveCourse(course).subscribe(
      () => console.log("Course saved")
    );
  }


}
