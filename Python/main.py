from Server import Server
import threading


def runner():
    server = Server()
    server.run()


if __name__ == '__main__':
    t = threading.Thread(target=runner)
    t.start()
    print("Running thread")
    t.join()
