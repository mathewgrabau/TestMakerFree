﻿import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "quiz-edit",
    templateUrl: "./quiz-edit.component.html",
    styleUrls: ['./quiz-edit.component.css']
})

export class QuizEditComponent {
    title: string;
    quiz: Quiz;

    // this will be TRUE when editing an existing quiz, FALSE when creating a new one.
    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        // create an empty quiz object from the quiz interface
        this.quiz = <Quiz>{};

        var id = +this.activatedRoute.snapshot.params['id'];
        if (id) {
            this.editMode = true;

            // Get the quiz from the server
            var url = this.baseUrl + "api/quiz/" + id;
            this.http.get<Quiz>(url).subscribe(res => {
                    this.quiz = res;
                    this.title = "Edit - " + this.quiz.Title;
                },
                error => console.error(error));
        } else {
            this.editMode = false;
            this.title = "Create a new quiz";
        }

    }

    onSubmit(quiz: Quiz) {
        var url = this.baseUrl + "api/quiz";

        if (this.editMode) {
            this.http.post<Quiz>(url, quiz)
                .subscribe(res => {
                        var v = res;
                        console.log("Quiz " + v.Id + " has been updated");
                        this.router.navigate(["home"]);
                    },
                    error => console.error(error));
        } else {
            this.http.put<Quiz>(url, quiz)
                .subscribe(res => {
                        var q = res;
                        console.log("Quiz " + q.Id + " has been created");
                        this.router.navigate(["home"]);
                    },
                    error => console.error(error));
        }
    }

    onBack() {
        this.router.navigate(["home"]);
    }
}
