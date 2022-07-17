package com.example.wearapp

import android.app.Activity
import android.os.Bundle
import com.example.step45kmm.Platform
import com.example.wearapp.databinding.ActivityMainBinding

class MainActivity : Activity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityMainBinding.inflate(layoutInflater)
        binding.text.text = Platform().platform
        setContentView(binding.root)

    }
}