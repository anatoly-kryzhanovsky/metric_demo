import time
from locust import HttpUser, task, constant_throughput

class QuickstartUser(HttpUser):
    wait_time = constant_throughput(3)

    @task
    def call_get_data(self):
        self.client.get("/data/data")
