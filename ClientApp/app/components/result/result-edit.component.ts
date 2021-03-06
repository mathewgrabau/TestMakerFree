﻿import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "result-edit",
    templateUrl: "./result-edit.component.html",
    styleUrls: ["./result-edit.component.css"]
})

export class ResultEditComponent {
    title: string;
    result: Result;

    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject("BASE_URL") private baseUrl: string) {

        // Empty result and will keep empty if creating a new result
        this.result = <Result>{};

        var id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

        if (this.editMode) {
            // fetch
            var url = this.baseUrl + "api/result/" + id;
            this.http.get<Result>(url).subscribe(res => {
                    this.result = res;
                    this.title = "Edit - " + this.result.Text;
                },
                error => console.error(error));
        } else {
            this.result.QuizId = id;
            this.title = "Create a new result";
        }
    }

    onSubmit(result: Result) {
        var url = this.baseUrl + "api/result";

        if (this.editMode) {
            this.http.post<Result>(url, result).subscribe(res => {
                    var v = res;
                    console.log("Result " + v.Id + " has been updated");
                    this.router.navigate(["quiz/edit", v.QuizId]);
                },
                error => console.error(error));
        } else {
            this.http.put<Result>(url, result).subscribe(res => {
                var v = res;
                console.log("Result " + v.Id + " has been created");
                this.router.navigate(["quiz/edit", v.QuizId]);
            })
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.result.QuizId]);
    }
}