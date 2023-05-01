import time
from locust import HttpUser, task, constant_throughput

class QuickstartUser(HttpUser):
    wait_time = constant_throughput(3)

    @task
    def call_job(self):
        self.client.post("/job/job1-bad")    
    